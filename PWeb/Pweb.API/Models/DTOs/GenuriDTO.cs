using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class GenuriDTO
    {
        [Key] public int genid { get; set; }

        public string numegen { get; set; }
    }
}
