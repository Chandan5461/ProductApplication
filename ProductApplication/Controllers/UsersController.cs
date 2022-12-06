using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Data;
using ProductApplication.Models.Domain;
using ProductApplication.Repostries;
using System.Data;

namespace ProductApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Manager")]

    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly PUserDbContext _context;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var Users = userRepository.GetAll();
            if (Users == null)
            {
                return NotFound("User Not found");
            }
            return Ok(Users);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetUsers")]

        public IActionResult GetUsers(int id)
        {
            var user = userRepository.Get(id);
            if (user == null)
            {
                return NotFound("User Not found");
            }
            return Ok(user);




        }

        [HttpPost]

        public IActionResult AddUser([FromBody] Models.DTO.AddUserRequest addUserRequest)
        {

            if (addUserRequest == null)
            {
                return NotFound("user not found");
            }
            var UserDomain = new Models.Domain.User()
            {
                Name = addUserRequest.Name,
                Address = addUserRequest.Address,
                City = addUserRequest.City,
                Role = addUserRequest.Role,
                Username = addUserRequest.Username,
                Password = addUserRequest.Password,

            };

            UserDomain = userRepository.Add(UserDomain);

            var userDTO = new Models.DTO.User
            {
                Id = UserDomain.Id,
                Name = UserDomain.Name,
                Address = UserDomain.Address,
                City = UserDomain.City,
                Role = UserDomain.Role,
                Username = UserDomain.Username,
                Password = UserDomain.Password,
            };

            return CreatedAtAction("GetUsers", new { id = userDTO.Id }, userDTO);
        }

        [HttpDelete]
        [Route("{id:int}")]

        public IActionResult DeleteUser(int id)
        {
            var userDomain = userRepository.Delete(id);

            if (userDomain == null)
            {
                return NotFound("User not found");
            }
            return Ok(userDomain);
        }


        [HttpPut]
        [Route("{id:int}")]

        public IActionResult UpdateUser([FromRoute] int id,
            [FromBody] Models.DTO.UpdateUserRequest updateUserRequest)
        {

            if (updateUserRequest == null)
            {
                return NotFound("user not found");
            }
            var userDomain = new Models.Domain.User
            {
                Name = updateUserRequest.Name,
                Address = updateUserRequest.Address,
                City = updateUserRequest.City,
                Role = updateUserRequest.Role,


            };

            userDomain = userRepository.Update(id, userDomain);

            if (userDomain == null)
            {
                return NotFound("User not found");
            }

            var userDTO = new Models.DTO.User
            {
                Id = userDomain.Id,
                Name = userDomain.Name,
                Address = userDomain.Address,
                City = userDomain.City,
                Role = userDomain.Role,
            };
            return Ok(userDTO);
        }

        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            var userAvailable = _context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (userAvailable != null)
            {
                return Ok("Success");
            }
            return NotFound("Failure");

        }


    }
}

