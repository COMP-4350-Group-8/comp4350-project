namespace SailMapper.Classes
{
    public class Boat
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Class { get; set; }
        public string? SailNumber { get; set; }
        public string? Skipper { get; set; }
        
        // Foreign key for Rating
        public int? RatingId { get; set; } // Nullable in case a Boat doesn't have a Rating

        public Rating? Rating { get; set; }
    }
}
