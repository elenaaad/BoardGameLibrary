using AutoMapper;
using BoardGameLibrary.Services.ProducerService;
using DAL.Models;
using DAL.Models.Dtos;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MusicCollection.Services.ProducerService
{
    public class ProducerService : IProducerService
    {
        public IProducerRepository _producerRepository;
        public IMapper _mapper;

        public ProducerService(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }

        public async Task<Producer> AddProducer(ProducerCreateDto newProducer)
        {
            var newProducerEntity = _mapper.Map<Producer>(newProducer);
            await _producerRepository.CreateAsync(newProducerEntity);
            await _producerRepository.SaveAsync();
            return newProducerEntity;
        }

        public async Task DeleteProducer(Guid producerId)
        {
            var producerToDelete = await _producerRepository.FindByIdAsync(producerId);
            _producerRepository.Delete(producerToDelete);
            await _producerRepository.SaveAsync();
        }

        public async Task<List<ProducerDto>> GetAll()
        {
            var producers = await _producerRepository.GetAll();
            return _mapper.Map<List<ProducerDto>>(producers);
        }

        public async Task<ProducerDto> GetProducerById(Guid producerId)
        {
            var producer = await _producerRepository.GetProducerById(producerId);
            return _mapper.Map<ProducerDto>(producer);
        }

        public async Task<Producer> GetProducerEntityById(Guid producerId)
        {
            var producer = await _producerRepository.GetProducerById(producerId);
            return producer;
        }
    }
}
