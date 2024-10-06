using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Race
    {
        [SetsRequiredMembers]
        public Race() 
        {
            Id = 0;
            Course = new Course();
        }
        public required int Id { get; set; }
        public required Course Course { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Boat> Participants { get; set; } = [];
    }
}
