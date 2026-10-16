using System.Net.Mail;
using StudentRecordSystem.Services;

namespace StudentRecordSystem.Models;

/// <summary>
/// A student and their unit enrolments. Inherits name handling from <see cref="Person"/>.
/// </summary>
public class Student : Person
{
    // The list itself is private; outside code gets a read-only view (see Enrolments)
    // and must use AddOrUpdateEnrolment/RemoveEnrolment to change it.
    private readonly List<Enrolment> _enrolments = new();

    private string _email = string.Empty;
    private string _courseCode = string.Empty;

    /// <summary>Creates a student. All values are validated.</summary>
    /// <exception cref="ArgumentException">Thrown when any value is invalid.</exception>
    public Student(string studentId, string firstName, string lastName, string email, string courseCode)
        : base(firstName, lastName) // calls the Person constructor first
    {
        StudentId = ValidateStudentId(studentId);
        Email = email;
        CourseCode = courseCode;
    }

    /// <summary>8-digit ECU student number. Get-only: it cannot change after creation.</summary>
    public string StudentId { get; }

    /// <summary>Student email address. Must be a valid address format.</summary>
    public string Email
    {
        get => _email;
        set
        {
            string email = RequireText(value, "Email");
            if (!MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("Email is not a valid email address.");
            }

            _email = email;
        }
    }

    /// <summary>Course the student is enrolled in, stored in upper case.</summary>
    public string CourseCode
    {
        get => _courseCode;
        set => _courseCode = RequireText(value, "Course code").ToUpperInvariant();
    }

    /// <summary>Read-only view of the enrolments.</summary>
    public IReadOnlyList<Enrolment> Enrolments => _enrolments;

    /// <summary>Weighted Average Mark, or null if the student has no marks yet.</summary>
    public decimal? Wam => AcademicCalculator.CalculateWam(_enrolments);

    /// <summary>
    /// Adds an enrolment, replacing any existing enrolment in the same unit.
    /// </summary>
    public void AddOrUpdateEnrolment(Unit unit, int? mark)
    {
        RemoveEnrolment(unit.UnitCode);
        _enrolments.Add(new Enrolment(unit, mark));
    }

    /// <summary>Removes the enrolment for a unit.</summary>
    /// <returns>True if an enrolment was removed.</returns>
    public bool RemoveEnrolment(string unitCode) =>
        _enrolments.RemoveAll(e => e.Unit.UnitCode == unitCode) > 0;

    /// <summary>Overrides the abstract method in <see cref="Person"/>.</summary>
    public override string GetRoleDescription() => $"Student enrolled in {CourseCode}";

    /// <summary>Checks the ID is exactly 8 digits.</summary>
    private static string ValidateStudentId(string? studentId)
    {
        string id = RequireText(studentId, "Student ID");
        if (id.Length != 8 || !id.All(char.IsDigit))
        {
            throw new ArgumentException("Student ID must be exactly 8 digits.");
        }

        return id;
    }
}
