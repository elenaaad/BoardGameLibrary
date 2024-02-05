using AutoMapper;
using BoardGameLibrary.Services.BoardgameService;
using DAL.Models;
using DAL.Models.Dtos;
using DAL.Repositories;

namespace BoardGameLibrary.Services.BoardGameService
{
    public class BoardGameService : IBoardGameService
    {
        public IBoardGameRepository _boardGameRepository;
        public IBoardGameInCollectionRepository _boardGameInCollectionRepository;
        public IMapper _mapper;
        public IProducerRepository _producerRepository;

        public BoardGameService(IBoardGameRepository boardGameRepository, IMapper mapper, IBoardGameInCollectionRepository boardGameInCollectionRepository, IProducerRepository producerRepository)
        {
            _boardGameRepository = boardGameRepository;
            _mapper = mapper;
            _boardGameInCollectionRepository = boardGameInCollectionRepository;
            _producerRepository = producerRepository;
        }
        //public Task<BoardGameDto> AddBoardGame(BoardGameCreateDto newBoardGame, Guid producerId)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<BoardGameDto> AddBoardGame(BoardGameCreateDto newBoardGame, Guid producerId)
        {
            var newBoardGameEntity = _mapper.Map<BoardGame>(newBoardGame);
            newBoardGameEntity.Producer = _producerRepository.FindById(producerId);
            newBoardGameEntity.ProducerId = producerId;
            await _boardGameRepository.CreateAsync(newBoardGameEntity);
            await _boardGameRepository.SaveAsync();
            var boardGameDto = _mapper.Map<BoardGameDto>(newBoardGameEntity);
            boardGameDto.ProducerName = await _producerRepository.GetProducerNameById(producerId);
            return boardGameDto;
        }

        public async Task DeleteBoardGame(Guid boardGameId)
        {
            var boardGameToDelete = await _boardGameRepository.FindByIdAsync(boardGameId);
            _boardGameRepository.Delete(boardGameToDelete);
            await _boardGameRepository.SaveAsync();
        }

        public async Task<List<BoardGameDto>> GetAll()
        {
            var boardGames = await _boardGameRepository.GetAll();
            return _mapper.Map<List<BoardGameDto>>(boardGames);
        }

        public async Task<BoardGameDto> GetBoardGameById(Guid boardGameId)
        {
            var boardGame = await _boardGameRepository.GetBoardGameById(boardGameId);
            return _mapper.Map<BoardGameDto>(boardGame);
        }

        public async Task AddBoardGameInCollection(Guid collectionId, Guid boardGameId)
        {
            await _boardGameInCollectionRepository.AddBoardGameInCollection(collectionId, boardGameId);
            await _boardGameInCollectionRepository.SaveAsync();
        }

        public async Task<List<BoardGameDto>> GetBoardGamesForCollection(Guid collectionId)
        {
            var boardGameIds = await _boardGameInCollectionRepository.GetBoardGameIdsForCollection(collectionId);
            var boardGames = await _boardGameRepository.GetBoardGamesByIds(boardGameIds);
            return _mapper.Map<List<BoardGameDto>>(boardGames);
        }

        public async Task<string> GetProducerName(BoardGameDto boardGameDto)
        {
            return await _producerRepository.GetProducerNameById(boardGameDto.ProducerId);
        }

       

        public Task<BoardGameDto> GetBoardgameById(Guid boardGameId)
        {
            throw new NotImplementedException();
        }
    }
}
