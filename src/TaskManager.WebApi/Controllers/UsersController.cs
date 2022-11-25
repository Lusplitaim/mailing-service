using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Application.DTO;
using TaskManager.Infrastructure.Data;
using AutoMapper;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private DatabaseContext _context;
        private IMapper _mapper;

        public UsersController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
        {
            var userRepository = _context.UserRepository;

            var users = await userRepository.GetUsers();

            return Ok(_mapper.Map<IEnumerable<AppUserDto>>(users));
        }
    }
}
