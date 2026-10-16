using Microsoft.Data.Sqlite;
using StudentRecordSystem.Exceptions;
using StudentRecordSystem.Models;

namespace StudentRecordSystem.Data;

/// <summary>
/// SQLite implementation of <see cref="IStudentRepository"/>.
/// </summary>
/// <remarks>
/// Microsoft.Data.Sqlite's *Async methods run synchronously, so each operation is
/// wrapped in Task.Run to move the database work off the UI thread.
/// </remarks>
public sealed class SqliteStudentRepository : IStudentRepository
{
    // SQLite error code for a PRIMARY KEY constraint failure.
    private const int SqlitePrimaryKeyViolation = 1555;

    private readonly string _connectionString;

    /// <summary>Creates the repository for the given SQLite database.</summary>
    public SqliteStudentRepository(string connectionString) => _connectionString = connectionString;

    /// <inheritdoc />
    public event EventHandler<StudentEventArgs>? StudentChanged;

    /// <inheritdoc />
    public Task<IReadOnlyList<Student>> GetAllStudentsAsync() => Task.Run<IReadOnlyList<Student>>(() =>
    {
        using SqliteConnection connection = OpenConnection();

        // List keeps the sorted order; Dictionary gives fast lookup by ID when adding enrolments.
        var students = new List<Student>();
        var studentsById = new Dictionary<string, Student>();

        using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText =
                "SELECT StudentId, FirstName, LastName, Email, CourseCode " +
                "FROM Students ORDER BY LastName, FirstName";

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var student = new Student(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4));
                students.Add(student);
                studentsById.Add(student.StudentId, student);
            }
        }

        using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = @"
                SELECT e.StudentId, u.UnitCode, u.UnitName, u.CreditPoints, e.Mark
                FROM Enrolments e
                JOIN Units u ON u.UnitCode = e.UnitCode
                ORDER BY u.UnitCode";

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // A NULL Mark column becomes a null int? (no result yet).
                int? mark = reader.IsDBNull(4) ? null : reader.GetInt32(4);
                var unit = new Unit(reader.GetString(1), reader.GetString(2), reader.GetInt32(3));

                if (studentsById.TryGetValue(reader.GetString(0), out Student? student))
                {
                    student.AddOrUpdateEnrolment(unit, mark);
                }
            }
        }

        return students;
    });

    /// <inheritdoc />
    public Task<IReadOnlyList<Unit>> GetAllUnitsAsync() => Task.Run<IReadOnlyList<Unit>>(() =>
    {
        using SqliteConnection connection = OpenConnection();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT UnitCode, UnitName, CreditPoints FROM Units ORDER BY UnitCode";

        var units = new List<Unit>();
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            units.Add(new Unit(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
        }

        return units;
    });

    /// <inheritdoc />
    public async Task AddStudentAsync(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        await Task.Run(() =>
        {
            using SqliteConnection connection = OpenConnection();
            using SqliteTransaction transaction = connection.BeginTransaction();

            try
            {
                using SqliteCommand command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO Students (StudentId, FirstName, LastName, Email, CourseCode) " +
                    "VALUES ($id, $first, $last, $email, $course)";
                AddStudentParameters(command, student);
                command.ExecuteNonQuery();
            }
            catch (SqliteException ex) when (ex.SqliteExtendedErrorCode == SqlitePrimaryKeyViolation)
            {
                // Exception filter ("when"): only primary key errors are translated;
                // any other database error keeps propagating unchanged.
                throw new DuplicateStudentException(student.StudentId, ex);
            }

            InsertEnrolments(connection, student);
            transaction.Commit();
        });

        // "await" resumes on the UI thread, so event handlers can update controls safely.
        OnStudentChanged(student.StudentId, StudentChangeType.Added);
    }

    /// <inheritdoc />
    public async Task UpdateStudentAsync(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        await Task.Run(() =>
        {
            using SqliteConnection connection = OpenConnection();
            using SqliteTransaction transaction = connection.BeginTransaction();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "UPDATE Students SET FirstName = $first, LastName = $last, " +
                    "Email = $email, CourseCode = $course WHERE StudentId = $id";
                AddStudentParameters(command, student);

                if (command.ExecuteNonQuery() == 0)
                {
                    throw new StudentNotFoundException(student.StudentId);
                }
            }

            // Simplest way to sync enrolments: delete the old rows and insert the current ones.
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Enrolments WHERE StudentId = $id";
                command.Parameters.AddWithValue("$id", student.StudentId);
                command.ExecuteNonQuery();
            }

            InsertEnrolments(connection, student);
            transaction.Commit(); // if an exception occurs before this, the transaction rolls back on dispose
        });

        OnStudentChanged(student.StudentId, StudentChangeType.Updated);
    }

    /// <inheritdoc />
    public async Task DeleteStudentAsync(string studentId)
    {
        await Task.Run(() =>
        {
            using SqliteConnection connection = OpenConnection();
            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Students WHERE StudentId = $id"; // enrolments cascade
            command.Parameters.AddWithValue("$id", studentId);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new StudentNotFoundException(studentId);
            }
        });

        OnStudentChanged(studentId, StudentChangeType.Deleted);
    }

    /// <summary>Opens a new connection. Each operation uses its own connection.</summary>
    private SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    /// <summary>Binds the student's fields to the $id, $first, $last, $email and $course parameters.</summary>
    private static void AddStudentParameters(SqliteCommand command, Student student)
    {
        command.Parameters.AddWithValue("$id", student.StudentId);
        command.Parameters.AddWithValue("$first", student.FirstName);
        command.Parameters.AddWithValue("$last", student.LastName);
        command.Parameters.AddWithValue("$email", student.Email);
        command.Parameters.AddWithValue("$course", student.CourseCode);
    }

    /// <summary>Inserts one Enrolments row per enrolment, reusing a single prepared command.</summary>
    private static void InsertEnrolments(SqliteConnection connection, Student student)
    {
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Enrolments (StudentId, UnitCode, Mark) VALUES ($id, $unit, $mark)";
        SqliteParameter unitParameter = command.Parameters.Add("$unit", SqliteType.Text);
        SqliteParameter markParameter = command.Parameters.Add("$mark", SqliteType.Integer);
        command.Parameters.AddWithValue("$id", student.StudentId);

        foreach (Enrolment enrolment in student.Enrolments)
        {
            unitParameter.Value = enrolment.Unit.UnitCode;
            // Null-coalescing operator: use DBNull when there is no mark.
            markParameter.Value = (object?)enrolment.Mark ?? DBNull.Value;
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Raises the StudentChanged event. The null-conditional operator (?.) skips
    /// the call when nothing has subscribed.
    /// </summary>
    private void OnStudentChanged(string studentId, StudentChangeType changeType) =>
        StudentChanged?.Invoke(this, new StudentEventArgs(studentId, changeType));
}
