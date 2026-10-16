using Microsoft.Data.Sqlite;
using StudentRecordSystem.Data;
using StudentRecordSystem.Exceptions;
using StudentRecordSystem.Models;
using StudentRecordSystem.Services;

namespace StudentRecordSystem.Forms;

/// <summary>
/// Dialog used by admins to add a new student or edit an existing one,
/// including the units they are enrolled in and their marks.
/// </summary>
public partial class StudentEditForm : Form
{
    private readonly IStudentRepository _repository;
    private readonly Student? _existing;

    // Working copy of the enrolments. Changes are only saved when Save is clicked.
    private readonly List<Enrolment> _enrolments;

    /// <summary>Creates the dialog.</summary>
    /// <param name="repository">Where the student is saved.</param>
    /// <param name="units">Units that can be chosen in the drop-down list.</param>
    /// <param name="existing">The student to edit, or null to add a new student.</param>
    public StudentEditForm(IStudentRepository repository, IReadOnlyList<Unit> units, Student? existing)
    {
        InitializeComponent();
        _repository = repository;
        _existing = existing;
        enrolmentsGrid.AutoGenerateColumns = false;
        unitComboBox.DataSource = units.ToList();

        if (existing is null)
        {
            Text = "Add Student";
            _enrolments = new List<Enrolment>();
        }
        else
        {
            Text = $"Edit Student - {existing.FullName}";
            studentIdTextBox.Text = existing.StudentId;
            studentIdTextBox.ReadOnly = true; // the ID is the primary key, so it cannot change
            firstNameTextBox.Text = existing.FirstName;
            lastNameTextBox.Text = existing.LastName;
            emailTextBox.Text = existing.Email;
            courseTextBox.Text = existing.CourseCode;
            _enrolments = existing.Enrolments.ToList();
        }

        RefreshEnrolmentsGrid();
    }

    /// <summary>Adds the chosen unit, or updates its mark if it is already in the list.</summary>
    private void AddUnitButton_Click(object? sender, EventArgs e)
    {
        if (unitComboBox.SelectedItem is not Unit unit)
        {
            return;
        }

        if (!AcademicCalculator.TryParseMark(markTextBox.Text, out int? mark))
        {
            ShowValidationError("Mark must be a whole number from 0 to 100, or left blank if the result is not released yet.");
            markTextBox.Focus();
            return;
        }

        _enrolments.RemoveAll(x => x.Unit.UnitCode == unit.UnitCode);
        _enrolments.Add(new Enrolment(unit, mark));
        markTextBox.Clear();
        RefreshEnrolmentsGrid();
    }

    /// <summary>Removes the unit selected in the grid.</summary>
    private void RemoveUnitButton_Click(object? sender, EventArgs e)
    {
        if (enrolmentsGrid.CurrentRow?.DataBoundItem is EnrolmentRow row)
        {
            _enrolments.RemoveAll(x => x.Unit.UnitCode == row.UnitCode);
            RefreshEnrolmentsGrid();
        }
    }

    /// <summary>
    /// Validates the input, builds a Student and saves it. The dialog stays open
    /// if anything is wrong so the user can correct it.
    /// </summary>
    private async void SaveButton_Click(object? sender, EventArgs e)
    {
        Student student;
        try
        {
            // The Student constructor and property setters validate every field
            // and throw ArgumentException with a readable message.
            student = new Student(studentIdTextBox.Text, firstNameTextBox.Text,
                lastNameTextBox.Text, emailTextBox.Text, courseTextBox.Text);
        }
        catch (ArgumentException ex)
        {
            ShowValidationError(ex.Message);
            return;
        }

        foreach (Enrolment enrolment in _enrolments)
        {
            student.AddOrUpdateEnrolment(enrolment.Unit, enrolment.Mark);
        }

        saveButton.Enabled = false;
        try
        {
            if (_existing is null)
            {
                await _repository.AddStudentAsync(student);
            }
            else
            {
                await _repository.UpdateStudentAsync(student);
            }

            DialogResult = DialogResult.OK; // closes the dialog
        }
        catch (DuplicateStudentException ex)
        {
            ShowValidationError(ex.Message);
            studentIdTextBox.Focus();
        }
        catch (StudentNotFoundException ex)
        {
            MessageBox.Show(this, $"{ex.Message}\nIt may have been deleted by another user.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.Cancel;
        }
        catch (SqliteException ex)
        {
            MessageBox.Show(this, $"Could not save the student.\n\n{ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            saveButton.Enabled = true;
        }
    }

    /// <summary>Shows the working enrolments, sorted by unit code, in the grid.</summary>
    private void RefreshEnrolmentsGrid()
    {
        enrolmentsGrid.DataSource = _enrolments
            .OrderBy(x => x.Unit.UnitCode)
            .Select(EnrolmentRow.From) // method group passed instead of a lambda
            .ToList();
    }

    /// <summary>Shows a validation warning.</summary>
    private void ShowValidationError(string message) =>
        MessageBox.Show(this, message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
