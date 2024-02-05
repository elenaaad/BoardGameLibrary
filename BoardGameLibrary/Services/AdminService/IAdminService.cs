using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.Dtos;
using Microsoft.Extensions.Hosting;

namespace Proiect.Services.AdminService
{
    public interface IAdminService
    {
        Task CreateProducer(ProducerCreateDto newProducer);
        Task DeleteProducer(Guid id);
        Task<UserDto> GetById(Guid id);
    }
}