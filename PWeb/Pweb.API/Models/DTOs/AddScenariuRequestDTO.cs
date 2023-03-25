using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class AddScenariuRequestDTO
    {
        [Key] public int scenariuid { get; set; }

        public string nume { get; set; }
    }
}
