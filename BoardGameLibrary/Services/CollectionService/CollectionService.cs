using AutoMapper;
using DAL.Models;
using DAL.Models.Dtos;
using DAL.Repositories;
using Microsoft.Identity.Client;
using BoardGameLibrary.Services.CollectionService;

namespace oardGameLibrary.Services.CollectionService
{
    public class CollectionService : ICollectionService
    {
        public ICollectionRepository _collectionRepository;
        public IBoardGameInCollectionRepository _boardGameInCollectionRepository;
        public IMapper _mapper;

        public CollectionService(ICollectionRepository collectionRepository, IMapper mapper, IBoardGameInCollectionRepository boardGameInCollectionRepository)
        {
            _collectionRepository = collectionRepository;
            _mapper = mapper;
            _boardGameInCollectionRepository = boardGameInCollectionRepository;
        }


        public async Task<Collection> AddCollection(CollectionCreateDto newCollection)
        {
            var newCollectionEntity = _mapper.Map<Collection>(newCollection);
            await _collectionRepository.CreateAsync(newCollectionEntity);
            await _collectionRepository.SaveAsync();
            return newCollectionEntity;
        }

        
        public async Task<List<CollectionDto>> GetCollectionsForUser(Guid userId)
        {
            var collections = await _collectionRepository.GetCollectionsForUser(userId);

            var collectionsDtos = _mapper.Map<List<CollectionDto>>(collections);

            foreach (var collectionDto in collectionsDtos)
            {
                collectionDto.numberOfBoardGames = await _boardGameInCollectionRepository.CountBoardGames(collectionDto.Id);
            }

            return collectionsDtos;
        }

        public async Task DeleteCollection(Guid collectionId)
        {
            var collectionToDelete = await _collectionRepository.FindByIdAsync(collectionId);
            _collectionRepository.Delete(collectionToDelete);
            await _collectionRepository.SaveAsync();
        }

        public async Task<List<CollectionDto>> GetAll()
        {
            var collections = await _collectionRepository.GetAll();
            return _mapper.Map<List<CollectionDto>>(collections);
        }

        public async Task<CollectionDto> GetCollectionById(Guid collectionId)
        {
            var collection = await _collectionRepository.GetCollectionById(collectionId);
            return _mapper.Map<CollectionDto>(collection);
        }

        public async Task<Collection> GetCollectionEntityById(Guid collectionId)
        {
            var collection = await _collectionRepository.GetCollectionById(collectionId);
            return collection;
        }

      
    }
}
