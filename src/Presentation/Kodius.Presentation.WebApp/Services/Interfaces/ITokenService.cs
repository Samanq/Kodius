using Kodius.Presentation.WebApp.Models;

namespace Kodius.Presentation.WebApp.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
    public RefreshToken GenerateRefreshToken();
}
