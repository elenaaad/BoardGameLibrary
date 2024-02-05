using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserDto(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = user.PasswordHash;
        }
    }

}