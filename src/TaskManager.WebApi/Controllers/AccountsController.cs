using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.WebApi.Interfaces;
using TaskManager.Application.DTO;
using TaskManager.Infrastructure.Data;

namespace TaskManager.WebApi.Controllers
{
    public class AccountsController : BaseApiController
    {
        private DatabaseContext _context;
        private ITokenService _tokenService;

        public AccountsController(DatabaseContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> SignIn([FromBody] SignInUserDto signInDto)
        {
            User? user = await _context.UserRepository.GetUserByEmail(signInDto.Email);
            if (user is null) return Unauthorized("No user with such email");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signInDto.Password));

            bool isWrongPassword = !user.PasswordHash.SequenceEqual(computedHash);
            if (isWrongPassword)
            {
                return Unauthorized("Wrong password");
            }

            return Ok(new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> SignUp([FromBody] SignUpUserDto signUpDto)
        {
            if (await UsernameExists(signUpDto.Username) || await EmailExists(signUpDto.Email))
            {
                return BadRequest("User already exists");
            }

            using var hmac = new HMACSHA512();

            User user = new()
            {
                Username = signUpDto.Username.ToLower(),
                Email = signUpDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUpDto.Password!)),
                PasswordSalt = hmac.Key,
            };

            User createdUser = await _context.UserRepository.CreateUser(user);

            return Ok(new UserDto
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Token = _tokenService.CreateToken(createdUser)
            });
        }

        private async Task<bool> UsernameExists(string username)
        {
            var user = await _context.UserRepository.GetUserByName(username);
            return user is not null;
        }

        private async Task<bool> EmailExists(string email)
        {
            var user = await _context.UserRepository.GetUserByEmail(email);
            return user is not null;
        }
    }
}
