namespace Kodius.Presentation.WebApp.Models;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
}
