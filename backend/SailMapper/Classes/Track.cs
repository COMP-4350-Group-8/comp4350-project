using System.Reflection.Metadata;

namespace SailMapper.Classes
{
    public class Track
    {
        public required int Id { get; set; }
        public required Boat Boat { get; set; }
        public Race? Race { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public float Distance { get; set; }
        public Blob GpxData { get; set; }
    }
}
