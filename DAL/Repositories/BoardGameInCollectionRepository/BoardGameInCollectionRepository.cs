using DAL.Data;
using DAL.Models;
using DAL.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DAL.Repositories
{
    public class BoardGameInCollectionRepository : GenericRepository<BoardGameInCollection>, IBoardGameInCollectionRepository
    {
        public BoardGameInCollectionRepository(BoardGameLibraryContext context) : base(context) { }

        public async Task AddBoardGameInCollection(Guid collectionId, Guid boardGameId)
        {
            BoardGameInCollection boardGameInCollection = new BoardGameInCollection();
            boardGameInCollection.CollectionId = collectionId;
            boardGameInCollection.BoardGameId = boardGameId;
            await CreateAsync(boardGameInCollection);
        }

        public async Task<List<Guid>> GetBoardGameIdsForCollection(Guid collectionId)
        {
            var boardGameIds = _table.Where(bgc => bgc.CollectionId == collectionId).Select(bgc => bgc.BoardGameId).ToList();
            return boardGameIds;
        }

        public async Task<int> CountBoardGames(Guid id)
        {
            return _table.Count(bgc => bgc.CollectionId == id);
        }
    }
}
