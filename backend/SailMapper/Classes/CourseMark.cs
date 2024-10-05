using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class CourseMark
    {
        [SetsRequiredMembers]
        public CourseMark() 
        {
            Id = 0;
        }
        public required int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool? Rounding { get; set; }
        public bool? IsStartLine{ get; set; }
        public CourseMark? Gate { get; set; }
        public Course? Course { get; set; }
        public int? CourseId { get; set; }

    }
}
