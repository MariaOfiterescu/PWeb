using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Roles { get; set; }
    }
}
