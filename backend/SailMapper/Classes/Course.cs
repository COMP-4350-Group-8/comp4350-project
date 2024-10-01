using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Course
    {
        [SetsRequiredMembers]
        public Course() 
        {
            Id = 0;
        }
        public required int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required CourseMark[] courseMarks { get; set; }
    }
}
