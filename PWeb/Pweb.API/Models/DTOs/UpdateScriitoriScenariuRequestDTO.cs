using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class UpdateScriitoriScenariuRequestDTO
    {
        [Key] public int scriitorid { get; set; }
        // public Scriitori Scriitori { get; set; }

        [Key] public int scenariuid { get; set; }

        //public Scenariu Scenariu { get; set; }
    }
}
