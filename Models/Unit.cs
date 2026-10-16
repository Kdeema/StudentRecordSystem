namespace StudentRecordSystem.Models;

/// <summary>
/// A unit of study, e.g. CSP3341. Declared as a positional record: the compiler
/// generates the constructor, read-only properties, value-based equality and ToString.
/// </summary>
/// <param name="UnitCode">Unique unit code, e.g. "CSP3341".</param>
/// <param name="UnitName">Full unit title.</param>
/// <param name="CreditPoints">Credit points, used to weight the WAM.</param>
public sealed record Unit(string UnitCode, string UnitName, int CreditPoints)
{
    /// <summary>Text shown when a unit is displayed in a ComboBox.</summary>
    public override string ToString() => $"{UnitCode} - {UnitName}";
}
