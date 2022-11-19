using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Data;
using TaskManager.WebApi.Models;

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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var userRepository = _context.UserRepository;

            var users = await userRepository.GetUsers();

            return Ok(users);
        }
    }
}
