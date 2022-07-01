namespace Kodius.Core.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; }
    public byte[] HashPassword { get; set; }
    public byte[] HashSalt { get; set; }
}
