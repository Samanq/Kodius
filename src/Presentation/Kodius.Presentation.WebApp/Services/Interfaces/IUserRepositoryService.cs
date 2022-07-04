using Kodius.Presentation.WebApp.Data;
using Kodius.Presentation.WebApp.Models;

namespace Kodius.Presentation.WebApp.Services.Interfaces
{
    public interface IUserRepositoryService
    {
        DataContext DataContext { get; set; }

        Task Add(User user);
        Task Delete(long id);
        Task Edit(User user);
        Task<User> Get(long id);
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
    }
}