using DAL.Models;
using DAL.Repository.GenericRepository;

namespace DAL.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindById(Guid id);
        User FindByEmail(string email);
        public Task<List<User>> GetUsers();
       
        int UserRole(Guid id);
       
    }
}