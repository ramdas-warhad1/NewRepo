using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{

    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User?> FindByNameAsync(string? userName);
    }
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
        }



        public async Task<User?> FindByNameAsync(string userName)
        {
            return await _context.users
                                 .FirstOrDefaultAsync(x => EF.Functions.Like(x.UserName, userName));
        }

    }
}
