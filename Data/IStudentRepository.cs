using StudentRecordSystem.Models;

namespace StudentRecordSystem.Data;

/// <summary>
/// Abstract data type for storing students. The forms only use this interface,
/// so they do not know (or care) that the data is kept in SQLite.
/// </summary>
public interface IStudentRepository
{
    /// <summary>Raised after a student is added, updated or deleted.</summary>
    event EventHandler<StudentEventArgs>? StudentChanged;

    /// <summary>Loads every student with their enrolments.</summary>
    Task<IReadOnlyList<Student>> GetAllStudentsAsync();

    /// <summary>Loads every unit a student can enrol in.</summary>
    Task<IReadOnlyList<Unit>> GetAllUnitsAsync();

    /// <summary>Saves a new student.</summary>
    /// <exception cref="Exceptions.DuplicateStudentException">The ID is already used.</exception>
    Task AddStudentAsync(Student student);

    /// <summary>Saves changes to an existing student.</summary>
    /// <exception cref="Exceptions.StudentNotFoundException">The student does not exist.</exception>
    Task UpdateStudentAsync(Student student);

    /// <summary>Deletes a student and their enrolments.</summary>
    /// <exception cref="Exceptions.StudentNotFoundException">The student does not exist.</exception>
    Task DeleteStudentAsync(string studentId);
}
