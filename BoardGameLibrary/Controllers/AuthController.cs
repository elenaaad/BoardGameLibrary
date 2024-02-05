using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using BoardGameLibrary.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

using DAL.Models.DTO;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("user")]
    [EnableCors("AllowAll")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpPost("create-user")]
    [EnableCors("AllowAll")]
    public async Task<IActionResult> CreateUser(UserAuthRequestDto user)
    {
        await _userService.Create(user);
        return Ok();
    }

    [HttpPost("login-user")]
    [EnableCors("AllowAll")]
    public IActionResult LoginUser(UserAuthRequestDto user)
    {
        var response = _userService.Authenticate(user);
        if (response == null)
        {
            return BadRequest("Username or password is invalid!");
        }
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("is-admin")]
    [EnableCors("AllowAll")]
    public IActionResult IsAdmin()
    {

        return Ok(true);
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("create-producer")]
    public async Task<IActionResult> CreateProducer(Producer producer)
    {

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-producer/{producerId}")]
    public async Task<IActionResult> DeletePost(Guid producerId)
    {

        return Ok();
    }

    //[Authorize(Roles = "User, Admin")]
    //[HttpPost("create-comment")]
    //public async Task<IActionResult> CreateComment(Comment comment)
    //{
    //    return Ok();
    //}


    //[Authorize(Roles = "Admin")]
    //[HttpDelete("delete-comment/{commentId}")]
    //public async Task<IActionResult> DeleteComment(Guid commentId)
    //{

    //    return Ok();
    //}
}