using Pweb.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class ScriitoriScenariuDTO
    {
        [Key] public int scriitorid { get; set; }
       // public Scriitori Scriitori { get; set; }

        [Key] public int scenariuid { get; set; }

        //public Scenariu Scenariu { get; set; }
    }
}
