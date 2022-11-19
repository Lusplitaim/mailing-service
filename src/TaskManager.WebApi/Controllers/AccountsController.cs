using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using TaskManager.WebApi.Data;
using TaskManager.WebApi.Models;
using TaskManager.WebApi.DTO;

namespace TaskManager.WebApi.Controllers
{
    public class AccountsController : BaseApiController
    {
        private DatabaseContext _context;

        public AccountsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> SignIn([FromBody] SignInUserDto signInDto)
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

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpUserDto signUpDto)
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

            return Ok(createdUser);
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
