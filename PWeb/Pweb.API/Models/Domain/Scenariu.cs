using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pweb.API.Models.Domain
{
    public class Scenariu
    {
        [Key] public int scenariuid { get; set; }

        public string nume { get; set; }


        //Navigation Property

        //un scenariu poate fi scris de mai multi scriitori

        public Filme filme { get; set; } // creez legatura cu tabela Filme

        public ICollection<ScriitoriScenariu> scriitoriscenariu { get; set; }

    }
}
