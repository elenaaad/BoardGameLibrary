using DAL.Models;
using DAL.Models.DTO;
using Newtonsoft.Json.Linq;

namespace BoardGameLibrary.Services
{
    public interface IAuthenticationService
    {
        Task<Token?> Authenticate(UserAuthRequestDto? user);
        Task<User> Register(UserAuthRequestDto user);
        void SendEmai(string email, string name);
        object GetById(int userId);
    }
}