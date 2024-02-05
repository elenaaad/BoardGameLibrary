using DAL.Data;
using DAL.Models;
using DAL.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BoardGameLibraryContext context) : base(context) { }

        public async Task<List<User>> GetUsers()
        {
            return await _table.ToListAsync();
        }

        public User FindByEmail(string email)
        {
            return _table.FirstOrDefault(x => x.Email == email);
        }

        public User FindById(Guid id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public int UserRole(Guid id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                return (int)user.Role;
            }
            return -1;
        }
    }
}
