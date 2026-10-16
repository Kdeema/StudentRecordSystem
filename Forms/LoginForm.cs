using Microsoft.Data.Sqlite;
using StudentRecordSystem.Data;
using StudentRecordSystem.Models;
using StudentRecordSystem.Services;

namespace StudentRecordSystem.Forms;

/// <summary>
/// First window shown. Checks the username and password, then opens the main window.
/// </summary>
public partial class LoginForm : Form
{
    private readonly AuthService _authService;
    private readonly IStudentRepository _repository;

    /// <summary>
    /// Dependencies are passed in through the constructor rather than created here.
    /// </summary>
    public LoginForm(AuthService authService, IStudentRepository repository)
    {
        InitializeComponent();
        _authService = authService;
        _repository = repository;
    }

    /// <summary>
    /// Click event handler for the Log In button. It is "async void" because
    /// WinForms event handlers must return void; await keeps the window responsive.
    /// </summary>
    private async void LoginButton_Click(object? sender, EventArgs e)
    {
        errorLabel.Text = string.Empty;
        loginButton.Enabled = false;

        try
        {
            User? user = await _authService.LoginAsync(usernameTextBox.Text, passwordTextBox.Text);

            if (user is null)
            {
                errorLabel.Text = "Incorrect username or password.";
                passwordTextBox.Clear();
                passwordTextBox.Focus();
                return;
            }

            OpenMainWindow(user);
        }
        catch (SqliteException ex)
        {
            errorLabel.Text = $"Database error: {ex.Message}";
        }
        finally
        {
            // finally always runs, even after return or an exception.
            loginButton.Enabled = true;
        }
    }

    /// <summary>
    /// Hides this window and shows the main window. When the main window closes
    /// (Sign Out), the login window is cleared and shown again.
    /// </summary>
    private void OpenMainWindow(User user)
    {
        var mainForm = new MainForm(user, _repository);

        // Lambda subscribed to the FormClosed event; the discards (_) ignore the unused parameters.
        mainForm.FormClosed += (_, _) =>
        {
            usernameTextBox.Clear();
            passwordTextBox.Clear();
            Show();
            usernameTextBox.Focus();
        };

        Hide();
        mainForm.Show();
    }
}
