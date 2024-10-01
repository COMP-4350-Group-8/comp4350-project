using SailMapper.Classes;

namespace SailMapper.Services
{
    public class CourseService
    {
        //Implement
        //return id
        public Task<string> AddCourse(Course course)
        {
            return Task.FromResult("");
        }

        //Implement
        //return list of courses, not full info
        public Task<Course[]> GetCourses()
        {
            return Task.FromResult(new Course[0]);
        }

        public Task<Course> GetCourse()
        {
            return Task.FromResult(new Course());
        }

        public Task<Course> GetCourse(string id)
        {
            return Task.FromResult(new Course());
        }

        public Task<bool> DeleteCourse(string id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> UpdateCourse(string id, Course course)
        {
            return Task.FromResult(false);
        }

        public Task<CourseMark[]> GetCourseMarks(string id)
        {
            return Task.FromResult(new CourseMark[0]);
        }

        public Task<string> AddMark(CourseMark mark)
        {
            return Task.FromResult("");
        }

        public Task<bool> DeleteCourseMark(string id, string markId)
        {
            return Task.FromResult(false);
        }

        public Task<bool> UpdateCourseMark(string id, CourseMark mark)
        {
            return Task.FromResult(false);
        }
    }
}
