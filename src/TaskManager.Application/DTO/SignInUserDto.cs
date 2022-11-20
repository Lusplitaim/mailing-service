using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTO
{
    public class SignInUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
