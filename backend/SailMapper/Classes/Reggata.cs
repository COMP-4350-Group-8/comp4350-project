namespace SailMapper.Classes
{
    public class Reggata
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Race[] Races { get; set; }
    }
}
