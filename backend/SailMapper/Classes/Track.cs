using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace SailMapper.Classes
{
    public class Track
    {
        [SetsRequiredMembers]
        public Track() { }
        public int Id { get; set; }
        public Boat? Boat { get; set; }
        public int? BoatId { get; set; }
        public Race? Race { get; set; }
        public int? RaceId { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
        public float? Distance { get; set; }
        public string? GpxData { get; set; }
    }
}
