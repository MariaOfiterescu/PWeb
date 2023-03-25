using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class ScenariuDTO
    {
        [Key] public int scenariuid { get; set; }

        public string nume { get; set; }
    }
}
