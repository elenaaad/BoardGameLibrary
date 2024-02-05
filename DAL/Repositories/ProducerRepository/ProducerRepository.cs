using DAL.Data;
using DAL.Models;
using DAL.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProducerRepository : GenericRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(BoardGameLibraryContext context) : base(context) { }

        public async Task<List<Producer>> GetProducers()
        {
            return await _table.ToListAsync();
        }

        public async Task<Producer> GetProducerById(Guid id)
        {
            return await FindByIdAsync(id);
        }

        public async Task<string> GetProducerNameById(Guid id)
        {
            Console.Write(_table.FirstOrDefault(p => p.Id == id).Name);
            return _table.FirstOrDefault(p => p.Id == id).Name;
        }
    }
}
