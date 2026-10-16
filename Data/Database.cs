using Microsoft.Data.Sqlite;
using StudentRecordSystem.Services;

namespace StudentRecordSystem.Data;

/// <summary>
/// Creates the SQLite database file, its tables, and the sample data on first run.
/// </summary>
public static class Database
{
    /// <summary>
    /// Connection string for "students.db", stored next to the .exe.
    /// Foreign keys are switched on so deleting a student also deletes their enrolments.
    /// </summary>
    public static string DefaultConnectionString { get; } = new SqliteConnectionStringBuilder
    {
        DataSource = Path.Combine(AppContext.BaseDirectory, "students.db"),
        ForeignKeys = true
    }.ToString();

    /// <summary>
    /// Creates any missing tables and adds sample data if the database is empty.
    /// Safe to call every time the program starts.
    /// </summary>
    public static void Initialise(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using SqliteCommand create = connection.CreateCommand();
        // Verbatim string (@"...") lets the SQL span several lines.
        create.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Username     TEXT PRIMARY KEY,
                PasswordHash TEXT NOT NULL,
                Salt         TEXT NOT NULL,
                FirstName    TEXT NOT NULL,
                LastName     TEXT NOT NULL,
                Role         TEXT NOT NULL CHECK (Role IN ('Admin', 'General'))
            );
            CREATE TABLE IF NOT EXISTS Students (
                StudentId  TEXT PRIMARY KEY,
                FirstName  TEXT NOT NULL,
                LastName   TEXT NOT NULL,
                Email      TEXT NOT NULL,
                CourseCode TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Units (
                UnitCode     TEXT PRIMARY KEY,
                UnitName     TEXT NOT NULL,
                CreditPoints INTEGER NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Enrolments (
                StudentId TEXT NOT NULL REFERENCES Students(StudentId) ON DELETE CASCADE,
                UnitCode  TEXT NOT NULL REFERENCES Units(UnitCode),
                Mark      INTEGER NULL CHECK (Mark BETWEEN 0 AND 100),
                PRIMARY KEY (StudentId, UnitCode)
            );";
        create.ExecuteNonQuery();

        if (IsEmpty(connection))
        {
            SeedSampleData(connection);
        }
    }

    /// <summary>True if no users exist yet (i.e. this is the first run).</summary>
    private static bool IsEmpty(SqliteConnection connection)
    {
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Users";
        return Convert.ToInt64(command.ExecuteScalar()) == 0;
    }

    /// <summary>
    /// Inserts two login accounts, sample units, students and marks inside one transaction,
    /// so either all of the sample data is saved or none of it is.
    /// </summary>
    private static void SeedSampleData(SqliteConnection connection)
    {
        using SqliteTransaction transaction = connection.BeginTransaction();

        AddUser(connection, "admin", "admin123", "Sarah", "Mitchell", "Admin");
        AddUser(connection, "user", "user123", "James", "Perera", "General");

        // Array of tuples keeps the sample data compact and readable.
        (string Code, string Name, int Credits)[] units =
        {
            ("CSP1150", "Programming Principles", 15),
            ("CSG1207", "Systems and Database Design", 15),
            ("CSP2108", "Introduction to Mobile Applications Development", 15),
            ("CSP3341", "Programming Languages and Paradigms", 15),
            ("CSI3345", "Industry Project", 30)
        };
        foreach (var unit in units)
        {
            Execute(connection, "INSERT INTO Units VALUES ($p0, $p1, $p2)", unit.Code, unit.Name, unit.Credits);
        }

        Execute(connection, "INSERT INTO Students VALUES ($p0, $p1, $p2, $p3, $p4)", "10648998", "Kusal", "Deemantha", "kusal@our.ecu.edu.au", "BCS");
        Execute(connection, "INSERT INTO Students VALUES ($p0, $p1, $p2, $p3, $p4)", "10612345", "Emily", "Nguyen", "emily@our.ecu.edu.au", "BIT");
        Execute(connection, "INSERT INTO Students VALUES ($p0, $p1, $p2, $p3, $p4)", "10655501", "Nimal", "Fernando", "nimal@our.ecu.edu.au", "BCS");
        Execute(connection, "INSERT INTO Students VALUES ($p0, $p1, $p2, $p3, $p4)", "10670022", "Olivia", "Brown", "olivia@our.ecu.edu.au", "BCY");

        const string enrol = "INSERT INTO Enrolments VALUES ($p0, $p1, $p2)";
        Execute(connection, enrol, "10648998", "CSP1150", 78);
        Execute(connection, enrol, "10648998", "CSG1207", 71);
        Execute(connection, enrol, "10648998", "CSP3341", null); // result not released yet
        Execute(connection, enrol, "10612345", "CSP1150", 85);
        Execute(connection, enrol, "10612345", "CSP2108", 66);
        Execute(connection, enrol, "10612345", "CSI3345", 74);
        Execute(connection, enrol, "10655501", "CSP1150", 48);
        Execute(connection, enrol, "10655501", "CSG1207", 59);
        Execute(connection, enrol, "10670022", "CSP3341", null);

        transaction.Commit();
    }

    /// <summary>Hashes the password and inserts a login account.</summary>
    private static void AddUser(SqliteConnection connection, string username, string password,
        string firstName, string lastName, string role)
    {
        // Tuple deconstruction: the two returned values go straight into two variables.
        (string hash, string salt) = PasswordHasher.HashPassword(password);
        Execute(connection, "INSERT INTO Users VALUES ($p0, $p1, $p2, $p3, $p4, $p5)",
            username, hash, salt, firstName, lastName, role);
    }

    /// <summary>
    /// Runs a SQL statement. The <c>params</c> keyword lets callers pass any number
    /// of values, which are bound to $p0, $p1, ... in order. Null becomes SQL NULL.
    /// </summary>
    private static void Execute(SqliteConnection connection, string sql, params object?[] values)
    {
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = sql;
        for (int i = 0; i < values.Length; i++)
        {
            command.Parameters.AddWithValue($"$p{i}", values[i] ?? DBNull.Value);
        }

        command.ExecuteNonQuery();
    }
}
