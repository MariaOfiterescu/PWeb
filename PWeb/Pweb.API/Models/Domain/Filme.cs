using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pweb.API.Models.Domain
{
    public class Filme
    {
        [Key] public int filmid { get; set; }

        public int directorid1{ get; set;}
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

        public string descriere { get; set;}

        public int anaparitie { get; set;}

        public string categorievarsta { get; set; }

        //Navigation Property

        // public List<Actori> Actori { get; set; } //un film are mai multi actori

        //public List<Genuri> Genuri { get; set; } //un film are mai multe genuri

        [Required]
        public Director director { get; set; }  

        public Scenariu scenariu { get; set; }

        [Required]

        public ICollection<FilmeGenuri> filmegenuri { get; set; }
        public ICollection<FilmeActori> filmeactori { get; set; }


    }
}
