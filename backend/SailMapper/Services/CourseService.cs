﻿using Microsoft.EntityFrameworkCore;
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
            return await _dbContext.Courses
                .ToListAsync();
        }

        public async Task<Course> GetCourse(int id)
        {
            return await _dbContext.Courses
                .Include(o => o.courseMarks)
                .Include(o => o.races)
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

        public async Task<bool> UpdateCourse(UpdateCourseDTO courseUpdate, int id)
        {
            var course = await _dbContext.Courses.FindAsync(id);
            if (course != null)
            {
                if (courseUpdate.Name != null) { course.Name = (string)courseUpdate.Name; }
                if (courseUpdate.Description != null) { course.Description = (string)courseUpdate.Description; }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CourseMark>> GetCourseMarks(int id)
        {
            return await _dbContext.CourseMarks
                .Where(c => c.CourseId == id)
                .ToListAsync();
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
                if (mark.GateId != null)
                {
                    CourseMark gate = await GetCourseMarkEntity((int)mark.GateId);
                    gate.GateId = null;
                }

                _dbContext.CourseMarks.Remove(mark);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCourseMark(UpdateCourseMarkDTO markUpdate, int id)
        {
            var mark = await _dbContext.CourseMarks.FindAsync(id);
            if (mark != null)
            {
                if (markUpdate.Latitude != null) { mark.Latitude = (float)markUpdate.Latitude; }
                if (markUpdate.Longitude != null) { mark.Longitude = (float)markUpdate.Longitude; }
                if (markUpdate.Description != null) { mark.Description = (string)markUpdate.Description; }
                if (markUpdate.Rounding != null) { mark.Rounding = (bool)markUpdate.Rounding; }
                if (markUpdate.IsStartLine != null) { mark.IsStartLine = (bool)markUpdate.IsStartLine; }
                if (markUpdate.GateId != null) { mark.GateId = (int)markUpdate.GateId; }
                if (markUpdate.CourseId != null) { mark.CourseId = (int)markUpdate.CourseId; }
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