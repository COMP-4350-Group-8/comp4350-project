namespace SailMapper.Classes
{
    public class CourseMark
    {
        public required int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool? Rounding { get; set; }
        public CourseMark? Gate { get; set; }
        public bool? IsStartLine{ get; set; }

    }
}
