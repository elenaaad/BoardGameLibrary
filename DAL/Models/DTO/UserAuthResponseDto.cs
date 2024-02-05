using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.DTO
{
    public class UserAuthResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public string Token { get; set; }

        public UserAuthResponseDto(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Token = token;
        }
    }
}