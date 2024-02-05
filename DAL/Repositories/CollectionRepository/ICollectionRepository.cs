using DAL.Models;
using DAL.Repository.GenericRepository;

namespace DAL.Repositories
{
    public interface ICollectionRepository : IGenericRepository<Collection>
    {
        Task<List<Collection>> GetCollections();

        Task<Collection> GetCollectionById(Guid Id);
        Task<List<Collection>> GetCollectionsForUser(Guid id);
    }
}
