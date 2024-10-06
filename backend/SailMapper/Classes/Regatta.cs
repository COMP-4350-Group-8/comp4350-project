using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Regatta
    {
        [SetsRequiredMembers]
        public Regatta() { }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Race>? Races { get; set; }
    }
}
