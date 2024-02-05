using DAL.Models;
using DAL.Repository.GenericRepository;

namespace DAL.Repositories
{
    public interface IBoardGameRepository : IGenericRepository<BoardGame>
    {
        Task<List<BoardGame>> GetBoardGames();

        Task<BoardGame> GetBoardGameById(Guid Id);
        Task<List<BoardGame>> GetBoardGamesByIds(List<Guid> boardGameIds);
    }
}
