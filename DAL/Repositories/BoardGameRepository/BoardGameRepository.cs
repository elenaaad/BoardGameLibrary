using DAL.Data;
using DAL.Models;
using DAL.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BoardGameRepository : GenericRepository<BoardGame>, IBoardGameRepository
    {
        private readonly BoardGameLibraryContext _dbContext;
        public BoardGameRepository(BoardGameLibraryContext context) : base(context) { _dbContext = context; }

        public async Task<List<BoardGame>> GetBoardGames()
        {
            return await _table.ToListAsync();
        }

        public async Task<BoardGame> GetBoardGameById(Guid id)
        {
            return await FindByIdAsync(id);
        }

        public async Task<List<BoardGame>> GetBoardGamesByIds(List<Guid> boardGameIds)
        {
            var boardGames = await _table.Where(boardGame => boardGameIds.Contains(boardGame.Id)).ToListAsync();
            return boardGames;
        }
    }
}
