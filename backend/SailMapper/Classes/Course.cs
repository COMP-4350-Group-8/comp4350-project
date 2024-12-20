﻿using System.Diagnostics.CodeAnalysis;

namespace SailMapper.Classes
{
    public class Course
    {
        [SetsRequiredMembers]
        public Course()
        {
            Id = 0;
            courseMarks = new List<CourseMark>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<CourseMark>? courseMarks { get; set; }
        public ICollection<Race>? races { get; set; }
    }
}