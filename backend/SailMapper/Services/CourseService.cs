using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{
    public class CourseService
    {
        private readonly SailDBContext _dbContext;

        public CourseService(SailDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddCourse(Course course)
        {
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            return course.Id;
        }

        //Implement
        //return list of courses, not full info
        public async Task<List<Course>> GetCourses()
        {
            List<Course> courses = await _dbContext.Courses
                .Include(o => o.courseMarks)
                .ToListAsync();

            return courses;
        }

        public async Task<Course> GetCourse(int id)
        {
            return await _dbContext.Courses
                .Include(o => o.courseMarks)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> DeleteCourse(int id)
        {
            Course course = await GetCourseEntity(id);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCourse(Course courseUpdate)
        {
            var course = _dbContext.Courses.Update(courseUpdate);
            if (course != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CourseMark>> GetCourseMarks(int id)
        {
            Course course = await GetCourseEntity(id);

            if (course != null && course.courseMarks != null)
            {
                return course.courseMarks.ToList();
            }
            return null;
        }

        public async Task<int> AddMark(CourseMark mark)
        {
            if (mark.CourseId.HasValue)
            {
                Course course = await GetCourseEntity((int)mark.CourseId);

                _dbContext.CourseMarks.Add(mark);
                course.courseMarks.Add(mark);
                await _dbContext.SaveChangesAsync();

                return mark.Id;
            }
            return -1;
        }

        public async Task<bool> DeleteCourseMark(int markId)
        {
            CourseMark mark = await GetCourseMarkEntity(markId);
            if (mark != null)
            {
                _dbContext.CourseMarks.Remove(mark);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCourseMark(CourseMark markUpdate)
        {
            var mark = _dbContext.CourseMarks.Update(markUpdate);
            if (mark != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<Course> GetCourseEntity(int id)
        {
            return await _dbContext.Courses.FindAsync(id);
        }
        private async Task<CourseMark> GetCourseMarkEntity(int id)
        {
            return await _dbContext.CourseMarks.FindAsync(id);
        }


    }
}