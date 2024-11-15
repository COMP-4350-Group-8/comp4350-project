using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SailMapper.Classes
{
    public class CourseMark
    {
        [SetsRequiredMembers]
        public CourseMark()
        {
            Id = 0;
        }
        public int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool? Rounding { get; set; }
        public bool? IsStartLine { get; set; }
        [JsonIgnore]
        public CourseMark? Gate { get; set; }
        public int? GateId { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }
        public int? CourseId { get; set; }

    }
}