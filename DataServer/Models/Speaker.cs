﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataServer.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(4000)]
        public string? Bio { get; set; }

        [StringLength(1000)]
        public virtual string? WebSite { get; set; }

        public List<SessionSpeaker> SessionSpeakers { get; set; } =
            new List<SessionSpeaker>();
    }
}
