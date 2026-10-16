namespace StudentRecordSystem.Models;

/// <summary>
/// ECU grade bands. The underlying int values are not marks; use
/// <see cref="Services.AcademicCalculator.GetGrade"/> to convert a mark to a grade.
/// </summary>
public enum Grade
{
    /// <summary>Fail (0-49).</summary>
    N,

    /// <summary>Pass (50-59).</summary>
    C,

    /// <summary>Credit (60-69).</summary>
    CR,

    /// <summary>Distinction (70-79).</summary>
    D,

    /// <summary>High Distinction (80-100).</summary>
    HD
}
