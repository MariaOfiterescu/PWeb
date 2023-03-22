using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pweb.API.Models.Domain
{
    public class Scriitori
    {
        [Key] public int scriitorid { get; set; } //primary key
/*
        public int scenariuid { get; set; }
        [ForeignKey("scenariuid")]

        public Scenariu Scenariuu { get; set; }
*/
        public string nume{ get; set; }

        public string prenume { get; set; }

        public int varsta { get; set; }

        //Navigation Property

        //public List<Scenariu> Scenariu { get; set; } //un scriitor poate scrie mai multe scenarii

        public ICollection<ScriitoriScenariu> scriitoriscenariu { get; set; }

    }
}
