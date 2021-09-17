using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataServer.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        //[UseUpperCase]
        public string? Title { get; set; }

        [StringLength(4000)]
        public string? Abstract { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public TimeSpan Duration =>
            EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ??
                TimeSpan.Zero;

        public int? TrackId { get; set; }

        public Track? Track { get; set; }

        public List<SessionSpeaker> SessionSpeakers { get; set; } =
            new List<SessionSpeaker>();

        public List<SessionAttendee> SessionAttendees { get; set; } =
            new List<SessionAttendee>();
    }
}
