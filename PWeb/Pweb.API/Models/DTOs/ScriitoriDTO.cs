using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class ScriitoriDTO
    {
        [Key] public int scriitorid { get; set; } //primary key
    
        public string nume { get; set; }

        public string prenume { get; set; }

        public int varsta { get; set; }
    }
}
