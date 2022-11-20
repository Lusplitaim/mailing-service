using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Data;
using TaskManager.Core.Models;
using TaskManager.Application.DTO;

namespace TaskManager.WebApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private DatabaseContext _context; 

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var userRepository = _context.UserRepository;

            var users = await userRepository.GetUsers();

            return Ok(users);
        }
    }
}
