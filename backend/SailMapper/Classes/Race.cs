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
        public ICollection<Boat>? Participants { get; set; } = new List<Boat>();
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Result>? Results { get; set; }
        public ICollection<Track>? Tracks { get; set; }
        public int RegattaId { get; set; }
        public Regatta Regatta { get; set; }

    }
}
