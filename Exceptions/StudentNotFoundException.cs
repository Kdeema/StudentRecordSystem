namespace StudentRecordSystem.Exceptions;

/// <summary>
/// Custom exception thrown when an update or delete targets a student ID
/// that is not in the database (for example, another user already deleted it).
/// </summary>
public class StudentNotFoundException : Exception
{
    /// <summary>Creates the exception for the given student ID.</summary>
    public StudentNotFoundException(string studentId)
        : base($"No student with ID {studentId} exists.")
    {
        StudentId = studentId;
    }

    /// <summary>The student ID that was not found.</summary>
    public string StudentId { get; }
}
