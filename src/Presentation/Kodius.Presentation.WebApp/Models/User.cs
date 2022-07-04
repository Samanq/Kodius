using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kodius.Presentation.WebApp.Models;


[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Required]
    [Key]
    public long Id { get; set; }

    [Required]
    [EmailAddress]

    public string Email { get; set; } = string.Empty;

    [Required]
    public byte[] HashPassword { get; set; }

    [Required]
    public byte[] HashSalt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryDate { get; set; }
}
