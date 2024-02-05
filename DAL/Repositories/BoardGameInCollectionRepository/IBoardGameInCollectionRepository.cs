using DAL.Models;
using DAL.Repository.GenericRepository;

namespace DAL.Repositories
{
    public interface IBoardGameInCollectionRepository : IGenericRepository<BoardGameInCollection>
    {
        Task AddBoardGameInCollection(Guid collectionId, Guid boardGameId);
        Task<List<Guid>> GetBoardGameIdsForCollection(Guid collectionId);
        Task<int> CountBoardGames(Guid id);
    }
}
