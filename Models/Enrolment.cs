using StudentRecordSystem.Services;

namespace StudentRecordSystem.Models;

/// <summary>
/// A student's enrolment in one unit, with an optional final mark.
/// </summary>
public sealed class Enrolment
{
    /// <summary>Creates an enrolment.</summary>
    /// <param name="unit">The unit the student is enrolled in.</param>
    /// <param name="mark">Final mark 0-100, or null if the result has not been released yet.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the mark is outside 0-100.</exception>
    public Enrolment(Unit unit, int? mark)
    {
        // Relational pattern: "is < 0 or > 100" is false when mark is null.
        if (mark is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(mark), mark, "Mark must be between 0 and 100.");
        }

        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        Mark = mark;
    }

    /// <summary>The enrolled unit.</summary>
    public Unit Unit { get; }

    /// <summary>Nullable value type: null means "no result yet".</summary>
    public int? Mark { get; }

    /// <summary>Grade derived from the mark, or null when there is no mark.</summary>
    public Grade? Grade => Mark.HasValue ? AcademicCalculator.GetGrade(Mark.Value) : null;
}
