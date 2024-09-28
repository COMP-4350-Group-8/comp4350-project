namespace SailMapper.Classes
{
    public class Result
    {
        public required Boat Boat { get; set; }
        public required Race race { get; set; }
        public required int FinishPosition { get; set; }
        public TimeOnly ElapsedTime { get; set; }
        public TimeOnly CorrectedTime { get; set; }
        public required int Rating { get; set; }
        public required int Points { get; set; }
        public string FinishType { get; set; } = string.Empty;
    }
}
