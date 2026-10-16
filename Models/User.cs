namespace StudentRecordSystem.Models;

/// <summary>
/// A staff member who can log in. The <see cref="Role"/> decides what they can do.
/// </summary>
public class User : Person
{
    /// <summary>Creates a user.</summary>
    public User(string username, string firstName, string lastName, Role role)
        : base(firstName, lastName)
    {
        Username = RequireText(username, "Username");
        Role = role;
    }

    /// <summary>Login name.</summary>
    public string Username { get; }

    /// <summary>Access level.</summary>
    public Role Role { get; }

    /// <summary>True when the user may change student records.</summary>
    public bool IsAdmin => Role == Role.Admin;

    /// <summary>
    /// Overrides the abstract method in <see cref="Person"/> using a switch expression.
    /// </summary>
    public override string GetRoleDescription() => Role switch
    {
        Role.Admin => "Administrator (full access)",
        Role.General => "General user (read-only access)",
        _ => "Unknown role"
    };
}
