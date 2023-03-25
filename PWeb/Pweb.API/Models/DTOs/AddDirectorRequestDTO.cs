using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class AddDirectorRequestDTO
    {
        [Key] public int directorid { get; set; }

        public string nume { get; set; }

        public string prenume { get; set; }
        public int varsta { get; set; }
    }
}
