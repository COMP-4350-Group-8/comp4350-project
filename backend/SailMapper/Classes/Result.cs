using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Result
    {
        [SetsRequiredMembers]
        public Result() { }
        public required Boat Boat { get; set; }
        public required Race race { get; set; }
        public required int FinishPosition { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public TimeSpan CorrectedTime { get; set; }
        public required int Rating { get; set; }
        public required int Points { get; set; }
        public string FinishType { get; set; } = string.Empty;
    }
}
