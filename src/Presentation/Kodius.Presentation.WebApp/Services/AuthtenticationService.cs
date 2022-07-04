using Kodius.Presentation.WebApp.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Kodius.Presentation.WebApp.Services;

public class AuthtenticationService : IAuthtenticationService
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwrodSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwrodSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
