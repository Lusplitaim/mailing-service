using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Application.DTO;
using TaskManager.Infrastructure.Data;

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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var userRepository = _context.UserRepository;

            var users = await userRepository.GetUsers();

            return Ok(users);
        }
    }
}
