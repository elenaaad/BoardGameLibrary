using DAL.Models;
using DAL.Models.Dtos;

namespace BoardGameLibrary.Services.ProducerService
{
    public interface IProducerService
    {
        public Task<List<ProducerDto>> GetAll();
        public Task<Producer> AddProducer(ProducerCreateDto newProducer);

        public Task DeleteProducer(Guid producerId);
        public Task<ProducerDto> GetProducerById(Guid producerId);
    }
}