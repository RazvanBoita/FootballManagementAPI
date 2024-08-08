using System.Security.Cryptography;
using System.Text;

namespace FootballMgm.Api.Utils;

public class PasswordManager
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
    
    public static bool VerifyPassword(string providedPassword, string storedHash)
    {
        var providedHash = HashPassword(providedPassword);
        return providedHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
    }
}