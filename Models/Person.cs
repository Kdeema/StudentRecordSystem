namespace StudentRecordSystem.Models;

/// <summary>
/// Abstract base class for every person stored in the system.
/// It cannot be instantiated directly; <see cref="Student"/> and <see cref="User"/>
/// inherit the shared name handling and must implement <see cref="GetRoleDescription"/>.
/// </summary>
public abstract class Person
{
    // Private backing fields: callers can only change the names through the
    // properties below, which validate the value first (encapsulation).
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    /// <summary>
    /// Constructor is protected so only derived classes can call it.
    /// </summary>
    protected Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>Given name. Cannot be empty.</summary>
    public string FirstName
    {
        get => _firstName;
        set => _firstName = RequireText(value, "First name");
    }

    /// <summary>Family name. Cannot be empty.</summary>
    public string LastName
    {
        get => _lastName;
        set => _lastName = RequireText(value, "Last name");
    }

    /// <summary>Read-only computed property (expression-bodied member).</summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Describes what this person is in the system. Each subclass provides its
    /// own version, so calling this through a <see cref="Person"/> reference is polymorphic.
    /// </summary>
    public abstract string GetRoleDescription();

    /// <summary>
    /// Shared validation helper for derived classes: rejects null/blank text and trims spaces.
    /// </summary>
    /// <param name="value">The text to check.</param>
    /// <param name="fieldName">Display name used in the error message.</param>
    /// <returns>The trimmed text.</returns>
    /// <exception cref="ArgumentException">Thrown when the text is null or blank.</exception>
    protected static string RequireText(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{fieldName} is required.");
        }

        return value.Trim();
    }
}
