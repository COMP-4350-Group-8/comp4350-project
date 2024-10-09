using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Regatta
    {
        [SetsRequiredMembers]
        public Regatta() { }
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ICollection<Race>? Races { get; set; }
    }
}