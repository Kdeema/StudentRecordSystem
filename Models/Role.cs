namespace StudentRecordSystem.Models;

/// <summary>
/// The access level of a person who logs in to the system.
/// </summary>
public enum Role
{
    /// <summary>Can view, search, add, edit and delete student records.</summary>
    Admin,

    /// <summary>Read-only access: can only view and search student records.</summary>
    General
}
