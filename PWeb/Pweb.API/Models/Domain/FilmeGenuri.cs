using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pweb.API.Models.Domain
{
    public class FilmeGenuri
    {
       // [Key] public int Id { get; set; } 

        [Key] public int filmid { get; set; } 
        public Filme Filme { get; set; }    

       [Key] public int genid { get; set; }

        public Genuri Genuri { get; set; }
        /*

        [Key] public int filmid { get; set; }
        [Key] public int genid { get; set; }

        internal int Filme { get; set; }
        internal int Genuri { get; set; }
        */
    }
}
