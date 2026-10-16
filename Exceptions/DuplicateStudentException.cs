namespace StudentRecordSystem.Exceptions;

/// <summary>
/// Custom exception thrown when adding a student whose ID already exists.
/// The original database error is kept as the InnerException.
/// </summary>
public class DuplicateStudentException : Exception
{
    /// <summary>Creates the exception for the given student ID.</summary>
    public DuplicateStudentException(string studentId, Exception innerException)
        : base($"A student with ID {studentId} already exists.", innerException)
    {
        StudentId = studentId;
    }

    /// <summary>The duplicated student ID.</summary>
    public string StudentId { get; }
}
