using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class UpdateDirectorRequestDTO
    {

        public string nume { get; set; }

        public string prenume { get; set; }
        public int varsta { get; set; }
    }
}
