using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class UpdateGenuriRequestDTO
    {
        [Key] public int genid { get; set; }

        public string numegen { get; set; }
    }
}
