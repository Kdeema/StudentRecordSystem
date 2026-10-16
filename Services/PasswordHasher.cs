using System.Security.Cryptography;

namespace StudentRecordSystem.Services;

/// <summary>
/// Hashes and checks passwords with PBKDF2 (SHA-256) and a random salt,
/// so plain-text passwords are never stored in the database.
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16;      // bytes
    private const int HashSize = 32;      // bytes
    private const int Iterations = 100_000; // digit separator improves readability

    /// <summary>Creates a new salted hash for a password.</summary>
    /// <returns>A tuple holding the Base64 hash and Base64 salt.</returns>
    public static (string Hash, string Salt) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    /// <summary>Checks a password against a stored hash and salt.</summary>
    /// <returns>True if the password is correct.</returns>
    public static bool Verify(string password, string storedHash, string storedSalt)
    {
        byte[] salt = Convert.FromBase64String(storedSalt);
        byte[] expected = Convert.FromBase64String(storedHash);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

        // Constant-time comparison so the time taken does not reveal how many bytes matched.
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
