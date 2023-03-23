using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
