using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SailMapper.Classes
{
    public class Result
    {
        [SetsRequiredMembers]
        public Result() { }
        public int Id { get; set; }
        [JsonIgnore]
        public required Boat Boat { get; set; }
        public int BoatId { get; set; }
        [JsonIgnore]
        public required Race Race { get; set; }
        public int RaceId { get; set; }

        public required int FinishPosition { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public TimeSpan CorrectedTime { get; set; }
        public required int Rating { get; set; }
        public required int Points { get; set; }
        public string FinishType { get; set; } = string.Empty;
    }
}