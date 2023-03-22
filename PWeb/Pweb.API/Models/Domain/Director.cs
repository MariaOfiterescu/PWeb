using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.Domain
{
    public class Director
    {
        [Key] public int directorid { get; set; }

        public string nume { get; set; }

        public string prenume { get; set; }
        public int varsta { get; set; }

        //Navigation Property

        // public List<Filme> Filme { get; set; } //un director are multiple filme in interior
        // 
     
        public ICollection<Filme> filme { get; set; }
        //public int Filme { get; internal set; }
    }
}
