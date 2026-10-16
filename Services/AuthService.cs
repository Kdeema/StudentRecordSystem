using Microsoft.Data.Sqlite;
using StudentRecordSystem.Models;

namespace StudentRecordSystem.Services;

/// <summary>
/// Checks login details against the Users table.
/// </summary>
public sealed class AuthService
{
    private readonly string _connectionString;

    /// <summary>Creates the service for the given SQLite database.</summary>
    public AuthService(string connectionString) => _connectionString = connectionString;

    /// <summary>
    /// Attempts to log in. Runs on a background thread (Task.Run) so the
    /// login window does not freeze while the password hash is calculated.
    /// </summary>
    /// <returns>The logged-in <see cref="User"/>, or null if the details are wrong.</returns>
    public Task<User?> LoginAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
        {
            return Task.FromResult<User?>(null);
        }

        return Task.Run<User?>(() =>
        {
            // "using var" disposes (closes) the connection automatically at the end of the block.
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            // Parameterised query: the username is never pasted into the SQL text,
            // which prevents SQL injection.
            command.CommandText =
                "SELECT Username, PasswordHash, Salt, FirstName, LastName, Role " +
                "FROM Users WHERE Username = $username";
            command.Parameters.AddWithValue("$username", username.Trim());

            using SqliteDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null; // unknown username
            }

            if (!PasswordHasher.Verify(password, reader.GetString(1), reader.GetString(2)))
            {
                return null; // wrong password
            }

            return new User(
                username: reader.GetString(0), // named arguments make each value's purpose clear
                firstName: reader.GetString(3),
                lastName: reader.GetString(4),
                role: Enum.Parse<Role>(reader.GetString(5)));
        });
    }
}
