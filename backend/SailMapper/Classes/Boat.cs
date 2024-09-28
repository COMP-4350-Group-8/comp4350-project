namespace SailMapper.Classes
{
    public class Boat
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Class { get; set; }
        public string? SailNumber { get; set; }
        public string? Skipper { get; set; }
        public Rating? Rating { get; set; }
    }
}
