using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.Domain
{
    public class FilmeActori
    {

        [Key] public int filmid { get; set; }
        public Filme Filme { get; set; }

        [Key] public int actorid { get; set; }

        public Actori Actori { get; set; }
    }
}
