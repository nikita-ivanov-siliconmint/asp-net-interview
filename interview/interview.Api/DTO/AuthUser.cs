using System.ComponentModel.DataAnnotations;

namespace interview.DTO
{
    public class AuthUser
    {
        [Required]
        [MinLength(5)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }
    }
}