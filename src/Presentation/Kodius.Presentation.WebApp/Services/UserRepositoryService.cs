using Kodius.Presentation.WebApp.Data;
using Kodius.Presentation.WebApp.Models;
using Kodius.Presentation.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kodius.Presentation.WebApp.Services;

public class UserRepositoryService : IUserRepositoryService
{
    public DataContext DataContext { get; set; }

    public UserRepositoryService(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await DataContext.Users.ToListAsync();
    }

    public async Task<User> Get(long id)
    {
        var user = await DataContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task Add(User user)
    {
        user.Email = user.Email.ToLower();
        
        await DataContext.Users.AddAsync(user);
        await DataContext.SaveChangesAsync();
    }

    public async Task Edit(User user)
    {
        DataContext.Entry(user).State = EntityState.Modified;
        await DataContext.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var user = await DataContext.Users.FindAsync(id);

        if (user != null)
        {
            DataContext.Remove(user);
            await DataContext.SaveChangesAsync();
        }
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await DataContext.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());

        return user;
    }
}
