using Microsoft.Data.Sqlite;
using StudentRecordSystem.Data;
using StudentRecordSystem.Forms;
using StudentRecordSystem.Services;

namespace StudentRecordSystem;

/// <summary>
/// Application entry point.
/// </summary>
static class Program
{
    /// <summary>
    /// Sets up the database and services, then shows the login window.
    /// [STAThread] is required by WinForms (single-threaded UI apartment).
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Last-resort handler for any exception not caught elsewhere on the UI thread,
        // so the user sees a message instead of the program crashing.
        Application.ThreadException += (_, e) =>
            MessageBox.Show($"An unexpected error occurred:\n\n{e.Exception.Message}",
                "Student Record System", MessageBoxButtons.OK, MessageBoxIcon.Error);

        string connectionString = Database.DefaultConnectionString;

        try
        {
            Database.Initialise(connectionString);
        }
        catch (SqliteException ex)
        {
            MessageBox.Show($"Could not open the database:\n\n{ex.Message}",
                "Student Record System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var authService = new AuthService(connectionString);
        IStudentRepository repository = new SqliteStudentRepository(connectionString);

        Application.Run(new LoginForm(authService, repository));
    }
}
