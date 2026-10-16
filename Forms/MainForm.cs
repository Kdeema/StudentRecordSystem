using Microsoft.Data.Sqlite;
using StudentRecordSystem.Data;
using StudentRecordSystem.Exceptions;
using StudentRecordSystem.Models;

namespace StudentRecordSystem.Forms;

/// <summary>
/// Main window. Every user can view and search students; only admins see the
/// Add / Edit / Delete buttons.
/// </summary>
public partial class MainForm : Form
{
    private readonly User _currentUser;
    private readonly IStudentRepository _repository;

    // In-memory copies of the data; searching filters these lists with LINQ.
    private List<Student> _students = new();
    private IReadOnlyList<Unit> _units = Array.Empty<Unit>();

    /// <summary>Creates the main window for the logged-in user.</summary>
    public MainForm(User currentUser, IStudentRepository repository)
    {
        InitializeComponent();
        _currentUser = currentUser;
        _repository = repository;

        // Columns are defined in the designer, so stop the grids adding extra ones.
        studentsGrid.AutoGenerateColumns = false;
        enrolmentsGrid.AutoGenerateColumns = false;

        welcomeLabel.Text = $"Welcome, {currentUser.FullName} - {currentUser.GetRoleDescription()}";

        // Role-based access: general users never see the buttons that change data.
        adminPanel.Visible = currentUser.IsAdmin;

        // Subscribe to the repository event so the list refreshes after any change.
        _repository.StudentChanged += Repository_StudentChanged;
    }

    /// <summary>
    /// Unsubscribes from the repository event when the window closes. The repository
    /// lives longer than this window, so leaving the handler attached would keep the
    /// closed window in memory.
    /// </summary>
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _repository.StudentChanged -= Repository_StudentChanged;
        base.OnFormClosed(e);
    }

    /// <summary>
    /// Load event: fetches students and units at the same time. Both tasks are started
    /// first, then Task.WhenAll waits for both, so the two queries run in parallel.
    /// </summary>
    private async void MainForm_Load(object? sender, EventArgs e)
    {
        SetBusy(true, "Loading student records...");

        try
        {
            Task<IReadOnlyList<Student>> studentsTask = _repository.GetAllStudentsAsync();
            Task<IReadOnlyList<Unit>> unitsTask = _repository.GetAllUnitsAsync();
            await Task.WhenAll(studentsTask, unitsTask);

            _students = studentsTask.Result.ToList(); // safe: the task has already completed
            _units = unitsTask.Result;
            ApplySearchFilter();
            statusLabel.Text = $"{_students.Count} student record(s) loaded.";
        }
        catch (SqliteException ex)
        {
            ShowError("load the student records", ex);
        }
        finally
        {
            SetBusy(false);
        }
    }

    /// <summary>Reloads the student list from the database.</summary>
    private async Task ReloadStudentsAsync()
    {
        try
        {
            _students = (await _repository.GetAllStudentsAsync()).ToList();
            ApplySearchFilter();
        }
        catch (SqliteException ex)
        {
            ShowError("reload the student records", ex);
        }
    }

    /// <summary>
    /// Filters the students by ID, name or course using a LINQ query with a lambda,
    /// then shows the results in the grid.
    /// </summary>
    private void ApplySearchFilter()
    {
        string term = searchTextBox.Text.Trim();

        IEnumerable<Student> results = string.IsNullOrEmpty(term)
            ? _students
            : _students.Where(s =>
                s.StudentId.Contains(term) ||
                s.FullName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                s.CourseCode.Contains(term, StringComparison.OrdinalIgnoreCase));

        studentsGrid.DataSource = results.ToList();
        ShowSelectedStudentEnrolments();
    }

    /// <summary>Shows the units of the selected student in the lower grid.</summary>
    private void ShowSelectedStudentEnrolments()
    {
        Student? selected = GetSelectedStudent();

        // Null-conditional (?.) and null-coalescing (??) operators avoid a NullReferenceException
        // when no student is selected.
        enrolmentsGrid.DataSource = selected?.Enrolments.Select(EnrolmentRow.From).ToList()
            ?? new List<EnrolmentRow>();
        enrolmentsLabel.Text = selected is null
            ? "Units for selected student:"
            : $"Units for {selected.FullName} (WAM: {selected.Wam?.ToString("F2") ?? "N/A"}):";
    }

    /// <summary>Returns the student in the selected grid row, or null.</summary>
    private Student? GetSelectedStudent() => studentsGrid.CurrentRow?.DataBoundItem as Student;

    /// <summary>TextChanged event: filters as the user types.</summary>
    private void SearchTextBox_TextChanged(object? sender, EventArgs e) => ApplySearchFilter();

    /// <summary>SelectionChanged event: updates the enrolments grid.</summary>
    private void StudentsGrid_SelectionChanged(object? sender, EventArgs e) => ShowSelectedStudentEnrolments();

    /// <summary>Double-clicking a row opens it for editing (admins only).</summary>
    private void StudentsGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (_currentUser.IsAdmin && e.RowIndex >= 0)
        {
            EditButton_Click(sender, e);
        }
    }

    /// <summary>Opens the edit dialog for a new student.</summary>
    private void AddButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new StudentEditForm(_repository, _units, existing: null);
        dialog.ShowDialog(this);
    }

    /// <summary>Opens the edit dialog for the selected student.</summary>
    private void EditButton_Click(object? sender, EventArgs e)
    {
        // "is not" pattern: stop if nothing is selected.
        if (GetSelectedStudent() is not Student selected)
        {
            MessageBox.Show(this, "Select a student to edit.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new StudentEditForm(_repository, _units, selected);
        dialog.ShowDialog(this);
    }

    /// <summary>Deletes the selected student after asking for confirmation.</summary>
    private async void DeleteButton_Click(object? sender, EventArgs e)
    {
        if (GetSelectedStudent() is not Student selected)
        {
            MessageBox.Show(this, "Select a student to delete.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        DialogResult answer = MessageBox.Show(this,
            $"Delete {selected.FullName} ({selected.StudentId}) and all of their enrolments?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        try
        {
            await _repository.DeleteStudentAsync(selected.StudentId);
        }
        catch (StudentNotFoundException ex)
        {
            // Someone else already deleted it: tell the user and refresh the list.
            MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            await ReloadStudentsAsync();
        }
        catch (SqliteException ex)
        {
            ShowError("delete the student", ex);
        }
    }

    /// <summary>
    /// Handler for the repository's custom StudentChanged event: reloads the list
    /// and reports what changed in the status bar.
    /// </summary>
    private async void Repository_StudentChanged(object? sender, StudentEventArgs e)
    {
        await ReloadStudentsAsync();
        statusLabel.Text = $"Student {e.StudentId} {e.ChangeType.ToString().ToLowerInvariant()}.";
    }

    /// <summary>Closes this window, which returns to the login window.</summary>
    private void SignOutButton_Click(object? sender, EventArgs e) => Close();

    /// <summary>Disables the window while data loads. The message parameter is optional.</summary>
    private void SetBusy(bool busy, string? message = null)
    {
        UseWaitCursor = busy;
        searchTextBox.Enabled = !busy;
        adminPanel.Enabled = !busy;
        if (message is not null)
        {
            statusLabel.Text = message;
        }
    }

    /// <summary>Shows a database error message.</summary>
    private void ShowError(string action, Exception ex) =>
        MessageBox.Show(this, $"Could not {action}.\n\n{ex.Message}", "Database Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
}
