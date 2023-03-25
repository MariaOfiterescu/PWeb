using Pweb.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class AddFilmeRequestDTO
    {
        [Key] public int filmid { get; set; }

        public int directorid1 { get; set; }
        [ForeignKey("directorid1")]
        // public Director Director { get; set; }

        public int genid { get; set; }
        [ForeignKey("genid")]
        public Genuri Gen { get; set; }

        public int actorid { get; set; }
        [ForeignKey("actorid")]
        public Actori Actor { get; set; }

        public int scenariuid1 { get; set; }
        [ForeignKey("scenariuid1")]
        // public Scenariu Scenariu { get; set; }

        public string titlu { get; set; }

        public string durata { get; set; }

        public string descriere { get; set; }

        public int anaparitie { get; set; }

        public string categorievarsta { get; set; }
    }
}
