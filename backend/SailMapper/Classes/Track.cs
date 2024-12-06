using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SailMapper.Classes
{
    public class Track
    {
        [SetsRequiredMembers]
        public Track() 
        {
            Id = 0;    
        }
        public int Id { get; set; }
        [JsonIgnore]
        public Boat? Boat { get; set; }
        public required int BoatId { get; set; }
        [JsonIgnore]
        public Race? Race { get; set; }
        public int RaceId { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public float Distance { get; set; }
        public required string GpxData { get; set; }
        [NotMapped]
        public int? CurrentRating { get; set; }
    }
}