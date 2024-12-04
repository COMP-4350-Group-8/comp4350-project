using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SailMapper.Classes
{
    public class UpdateCourseMarkDTO
    {
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string? Description { get; set; } = string.Empty;
        public bool? Rounding { get; set; }
        public bool? IsStartLine { get; set; }
        public int? GateId { get; set; }
        public int? CourseId { get; set; }

    }
}