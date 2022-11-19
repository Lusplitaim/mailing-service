using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebApi.DTO
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
