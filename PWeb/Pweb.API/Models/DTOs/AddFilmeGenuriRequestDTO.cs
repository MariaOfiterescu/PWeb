using Pweb.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class AddFilmeGenuriRequestDTO
    {

        [Key] public int filmid { get; set; }
       // public Filme Filme { get; set; }

        [Key] public int genid { get; set; }

        //public Genuri Genuri { get; set; }
    }
}
