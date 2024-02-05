using DAL.Models.DTO;
using DAL.Models;
using DAL.Repositories.UnitOfWork;

using Microsoft.AspNetCore.Authorization;
using BoardGameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using DAL.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using IAuthenticationService = BoardGameLibrary.Services.IAuthenticationService;

namespace BoardGameLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _service;

        private readonly BoardGameLibraryContext _proiectContext;
        public UserController(IUnitOfWork unitOfWork, IAuthenticationService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }


        [HttpGet]

        [EnableCors("AllowAll")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = (await _unitOfWork.Users.GetAll()).Select(a => new UserDto(a)).ToList();
            return users;
        }


        [HttpPost("Login")]
        [EnableCors("AllowAll")]
        public async Task<IActionResult> Authenticate(UserAuthRequestDto user)
        {
            Token? token;
            try
            {
                token = await _service.Authenticate(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("Register")]
        public async Task<IActionResult> PostUser(UserAuthRequestDto user)
        {
            try
            {
                var newUser = _service.Register(user);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userInDb = _unitOfWork.Users.FindById(id);

            if (userInDb == null)
            {
                return NotFound("User with this id doesn't exist");
            }

            _unitOfWork.Users.Delete(userInDb);
            _unitOfWork.Save();

            return Ok();
        }
    }
}