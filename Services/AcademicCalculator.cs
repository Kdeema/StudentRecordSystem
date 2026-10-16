using StudentRecordSystem.Models;

namespace StudentRecordSystem.Services;

/// <summary>
/// Static helper methods for marks, grades and WAM. A static class cannot be
/// instantiated and holds no state, so these methods behave like pure functions.
/// </summary>
public static class AcademicCalculator
{
    /// <summary>Lowest mark that counts as a pass.</summary>
    public const int PassMark = 50;

    /// <summary>
    /// Converts a mark to an ECU grade using a switch expression with relational patterns.
    /// Patterns are checked top to bottom, so the first matching band wins.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the mark is outside 0-100.</exception>
    public static Grade GetGrade(int mark) => mark switch
    {
        < 0 or > 100 => throw new ArgumentOutOfRangeException(nameof(mark), mark, "Mark must be between 0 and 100."),
        >= 80 => Grade.HD,
        >= 70 => Grade.D,
        >= 60 => Grade.CR,
        >= PassMark => Grade.C,
        _ => Grade.N
    };

    /// <summary>
    /// Calculates the Weighted Average Mark using LINQ:
    /// WAM = sum(mark x credit points) / sum(credit points), using only units that have a mark.
    /// </summary>
    /// <returns>The WAM rounded to 2 decimal places, or null if no unit has a mark.</returns>
    public static decimal? CalculateWam(IEnumerable<Enrolment> enrolments)
    {
        List<Enrolment> marked = enrolments.Where(e => e.Mark.HasValue).ToList();
        if (marked.Count == 0)
        {
            return null;
        }

        // decimal is used instead of double so the result has no binary rounding error.
        decimal totalCredits = marked.Sum(e => e.Unit.CreditPoints);
        decimal weightedTotal = marked.Sum(e => (decimal)e.Mark!.Value * e.Unit.CreditPoints);

        return Math.Round(weightedTotal / totalCredits, 2);
    }

    /// <summary>
    /// Tries to read a mark typed by the user. Uses an <c>out</c> parameter so the
    /// method can return both "did it work?" and the parsed value.
    /// </summary>
    /// <param name="text">The text entered. Blank means "no mark yet".</param>
    /// <param name="mark">The parsed mark, or null when the text is blank.</param>
    /// <returns>True if the text is blank or a whole number from 0 to 100.</returns>
    public static bool TryParseMark(string? text, out int? mark)
    {
        mark = null;

        if (string.IsNullOrWhiteSpace(text))
        {
            return true;
        }

        if (int.TryParse(text.Trim(), out int value) && value is >= 0 and <= 100)
        {
            mark = value;
            return true;
        }

        return false;
    }
}
