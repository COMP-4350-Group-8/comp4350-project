﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SailMapper.Classes
{
    public class Race
    {
        [SetsRequiredMembers]
        public Race()
        {
            Id = 0;
        }
        public required int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Boat>? Participants { get; set; } = new List<Boat>();
        public ICollection<Result>? Results { get; set; }
        public ICollection<Track>? Tracks { get; set; }
        public int? RegattaId { get; set; }
        [JsonIgnore]
        public Regatta? Regatta { get; set; }
        public int? CourseId { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }

    }
}