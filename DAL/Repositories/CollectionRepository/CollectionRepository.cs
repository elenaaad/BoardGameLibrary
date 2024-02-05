using DAL.Data;
using DAL.Models;
using DAL.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DAL.Repositories
{
    public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(BoardGameLibraryContext context) : base(context) { }

        public async Task<List<Collection>> GetCollections()
        {
            return await GetAll();
        }

        public async Task<Collection> GetCollectionById(Guid id)
        {
            return await FindByIdAsync(id);
        }

        public async Task<List<Collection>> GetCollectionsForUser(Guid id)
        {
            return await _table.Where(c => c.UserId == id).AsNoTracking().ToListAsync();
        }
    }
}
