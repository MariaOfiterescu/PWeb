using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.Domain
{
    public class Actori
    {
        [Key] public int actorid { get; set; }
        public  string nume { get; set; }

        public string prenume { get; set; }

        public string nationalitate { get; set; }

        public string datanastere { get; set; }

        //Navigation Property

        //public List<Filme> Filme { get; set; } //un actor are multiple filme in interior 

        public ICollection<FilmeActori> filmeactori { get; set; }
    }
}
