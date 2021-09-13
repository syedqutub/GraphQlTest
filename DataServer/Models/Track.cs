using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataServer.Models
{
    public class Track
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public List<Session> Sessions { get; set; } =
            new List<Session>();
    }
}
