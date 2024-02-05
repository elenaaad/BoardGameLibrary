using DAL.Models;
using DAL.Repository.GenericRepository;

namespace DAL.Repositories
{
    public interface IProducerRepository : IGenericRepository<Producer>
    {
        Task<List<Producer>> GetProducers();

        Task<Producer> GetProducerById(Guid Id);
        Task<string> GetProducerNameById(Guid id);
    }
}
