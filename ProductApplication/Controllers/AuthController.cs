using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Models.Domain;
using ProductApplication.Repostries;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public IActionResult Login(Login user)
        {
            var result = _userRepository.Authenticate(user);
            if (result == null)
            {
                return NotFound("username or password is incorrect");
            }
            return Ok(_tokenHandler.CreateToken(result));
        }
    }
}