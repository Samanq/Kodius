namespace Kodius.Presentation.WebApp.Services.Interfaces;

public interface IAuthtenticationService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwrodSalt);

    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
