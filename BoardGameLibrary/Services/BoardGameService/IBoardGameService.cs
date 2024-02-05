using DAL.Models;
using DAL.Models.Dtos;

namespace BoardGameLibrary.Services.BoardgameService
{
    public interface IBoardGameService
    {
        public Task<List<BoardGameDto>> GetAll();
        public Task<BoardGameDto> AddBoardGame(BoardGameCreateDto newBoardGame, Guid producerId);

        public Task DeleteBoardGame(Guid boardGameId);
        public Task<BoardGameDto> GetBoardGameById(Guid boardGameId);
        public Task AddBoardGameInCollection(Guid collectionId, Guid boardGameId);
        public Task<List<BoardGameDto>> GetBoardGamesForCollection(Guid collectionId);
        public Task<string> GetProducerName(BoardGameDto boardGameDto);
    }
}
