﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SailMapper.Classes
{
    public class Boat
    {
        [SetsRequiredMembers]
        public Boat()
        {
            Id = 0;
        }
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Class { get; set; }

        //Should change to int
        public string? SailNumber { get; set; }
        public string? Skipper { get; set; }

        // Foreign key for Rating
        public int? RatingId { get; set; } // Nullable in case a Boat doesn't have a Rating
        [JsonIgnore]
        public Rating? Rating { get; set; }

        public ICollection<Result>? Results { get; set; }
        public ICollection<Track>? Tracks { get; set; }
        public ICollection<Race>? Races { get; set; } = new List<Race>();
    }
}