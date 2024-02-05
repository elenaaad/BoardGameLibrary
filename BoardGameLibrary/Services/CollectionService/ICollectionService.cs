using DAL.Models;
using DAL.Models.Dtos;

namespace BoardGameLibrary.Services.CollectionService
{
    public interface ICollectionService
    {
        Task<List<CollectionDto>> GetAll();
        Task<Collection> AddCollection(CollectionCreateDto newCollection);
        Task DeleteCollection(Guid collectionId);
        Task<CollectionDto> GetCollectionById(Guid collectionId);
        Task<List<CollectionDto>> GetCollectionsForUser(Guid userId);
        
    }
}
