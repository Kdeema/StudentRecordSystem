using StudentRecordSystem.Models;

namespace StudentRecordSystem.Forms;

/// <summary>
/// Flat, display-ready version of an <see cref="Enrolment"/> for binding to a DataGridView,
/// which cannot show nested properties such as Enrolment.Unit.UnitCode directly.
/// </summary>
public sealed record EnrolmentRow(string UnitCode, string UnitName, int CreditPoints, string Mark, string Grade)
{
    /// <summary>Converts an enrolment into a row, showing "Pending" when there is no mark yet.</summary>
    public static EnrolmentRow From(Enrolment enrolment) => new(
        enrolment.Unit.UnitCode,
        enrolment.Unit.UnitName,
        enrolment.Unit.CreditPoints,
        enrolment.Mark?.ToString() ?? "Pending",
        enrolment.Grade?.ToString() ?? "-");
}
