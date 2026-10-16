namespace StudentRecordSystem.Data;

/// <summary>The kind of change made to a student record.</summary>
public enum StudentChangeType
{
    /// <summary>A new student was added.</summary>
    Added,

    /// <summary>An existing student was updated.</summary>
    Updated,

    /// <summary>A student was deleted.</summary>
    Deleted
}

/// <summary>
/// Data sent with the <see cref="IStudentRepository.StudentChanged"/> event.
/// </summary>
public class StudentEventArgs : EventArgs
{
    /// <summary>Creates the event data.</summary>
    public StudentEventArgs(string studentId, StudentChangeType changeType)
    {
        StudentId = studentId;
        ChangeType = changeType;
    }

    /// <summary>ID of the student that changed.</summary>
    public string StudentId { get; }

    /// <summary>What happened to the student.</summary>
    public StudentChangeType ChangeType { get; }
}
