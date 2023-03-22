using System.ComponentModel.DataAnnotations;

namespace Pweb.API.Models.DTOs
{
    public class ActoriDTO
    {
        [Key] public int actorid { get; set; }
        public string nume { get; set; }

        public string prenume { get; set; }

        public string nationalitate { get; set; }

        public string datanastere { get; set; }

    }
}
