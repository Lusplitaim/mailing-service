using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebApi.DTO
{
    public class SignUpUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
