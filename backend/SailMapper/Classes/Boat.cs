using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Boat
    {
        [SetsRequiredMembers]
        public Boat() 
        {
            Id = 0;
        }
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Class { get; set; }
        public string? SailNumber { get; set; }
        public string? Skipper { get; set; }
        public Rating? Rating { get; set; }
    }
}
