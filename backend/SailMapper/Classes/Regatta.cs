namespace SailMapper.Classes
{
    public class Regatta
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Race[] Races { get; set; }
    }
}
