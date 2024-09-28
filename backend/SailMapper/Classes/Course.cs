namespace SailMapper.Classes
{
    public class Course
    {
        public required int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required CourseMark[] courseMarks { get; set; }
    }
}
