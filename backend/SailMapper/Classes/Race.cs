namespace SailMapper.Classes
{
    public class Race
    {
        public required int Id { get; set; }
        public required Course Course { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public required string? Participants { get; set; } 
        
    }
}
