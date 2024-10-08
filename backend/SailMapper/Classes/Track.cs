﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace SailMapper.Classes
{
    public class Track
    {
        [SetsRequiredMembers]
        public Track() { }
        public required int Id { get; set; }
        public required Boat Boat { get; set; }
        public int BoatId { get; set; }
        public Race? Race { get; set; }
        public int RaceId { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public float Distance { get; set; }
        public required string GpxData { get; set; }
    }
}