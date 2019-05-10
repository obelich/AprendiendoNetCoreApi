using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto UserForRegisterDto)
        {
            UserForRegisterDto.Username = UserForRegisterDto.Username.ToLower();

            if (await _repo.UserExist(UserForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = UserForRegisterDto.Username
            };
            
            var CreatedUser = await _repo.Register(userToCreate, UserForRegisterDto.Password);

            // return CreatedAtRouteResult()

            return StatusCode(201);

        }

    }
}