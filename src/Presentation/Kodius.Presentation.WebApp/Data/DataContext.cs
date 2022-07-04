using Kodius.Presentation.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Kodius.Presentation.WebApp.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) 
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}
