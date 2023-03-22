using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.Domain
{
    public class Genuri
    {
        [Key] public int genid { get; set; }

        public string numegen { get; set; }

        //Navigation Property

        //public List<Filme> Filme { get; set; } //un gen apare la mai multe filme
        

       // public ICollection<Filme> filme { get; set; }
        public ICollection<FilmeGenuri> filmegenuri { get; set; }

    }
}
