using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Result
    {
        [SetsRequiredMembers]
        public Result() { }
        public int Id { get; set; }
        public required Boat Boat { get; set; }
        public int BoatId { get; set; }
        public required Race Race { get; set; }
        public int RaceId { get; set; }

        public required int FinishPosition { get; set; }
        public TimeOnly ElapsedTime { get; set; }
        public TimeOnly CorrectedTime { get; set; }
        public required int Rating { get; set; }
        public required int Points { get; set; }
        public string FinishType { get; set; } = string.Empty;
    }
}
