using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Controllers;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    public class CourseTests : IDisposable
    {
        private readonly CourseController _controller;
        private readonly CourseService _service;
        private readonly SailDBContext _dbContext;

        public CourseTests()
        {
            _dbContext = CreateDB.InitalizeDB();
            _controller = new CourseController(_dbContext);
            _service = new CourseService(_dbContext);
        }

        public void Dispose()
        {
            CreateDB.DeleteTempDB(_dbContext);
        }

        [Fact]
        public async Task Get_ReturnsListOfCourses()
        {
            var okResult = await _controller.GetCourses();

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Cont_GetCourse()
        {
            var ok = await _controller.GetCourse(0);

            Assert.IsType<NotFoundResult>(ok);
        }


        [Fact]
        public async Task Service_ReturnsListOfCourses()
        {
            var result = await _service.GetCourses();

            Assert.IsType<List<Course>>(result);
        }

        [Fact]
        public async Task Add_Course()
        {
            Course course = new Course 
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                courseMarks = null
            };
            await _controller.CreateCourse(course);

            var retVal = await _dbContext.Courses.FindAsync(1);
            Assert.NotNull(retVal);
            Assert.Equal(1, retVal.Id);
            Assert.Equal("Test", retVal.Name);
            Assert.Equal("Test", retVal.Description);
        }

        [Fact]
        public async Task Update_Course()
        {
            Course course = new Course
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                courseMarks = null
            };
            await _controller.CreateCourse(course);
            var retVal = await _dbContext.Courses.FindAsync(1);
            Assert.NotNull(retVal);

            await _controller.UpdateCourse(1, new UpdateCourseDTO { Name = "Update" });

            Assert.Equal("Update", retVal.Name);
        }
        [Fact]
        public async Task Delete_Course()
        {
            Course course = new Course
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                courseMarks = null
            };
            await _controller.CreateCourse(course);
            var retVal = await _dbContext.Courses.FindAsync(1);
            Assert.NotNull(retVal);

            await _controller.DeleteCourse(1);

            Assert.Null(await _dbContext.Courses.FindAsync(1));
        }

        [Fact]
        public async Task Get_CourseMarks()
        {
            var ok = await _controller.GetCourseMarks(1);

            Assert.IsType<OkObjectResult>(ok);
        }

        [Fact]
        public async Task Add_CourseMark()
        {
            Course course = new Course
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                courseMarks = null
            };
            await _controller.CreateCourse(course);

            CourseMark mark = new CourseMark
            {
                Id = 1,
                Latitude = 0,
                Longitude = 0,
                Description = "Test",
                CourseId = 1
            };
            await _controller.CreateMark(mark);

            var retVal = await _dbContext.CourseMarks.FindAsync(1);
            Assert.NotNull(retVal);
            Assert.Equal(1, retVal.Id);
            Assert.Equal("Test", retVal.Description);
        }

        [Fact]
        public async Task Update_CourseMark()
        {
            Course course = new Course
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                courseMarks = null
            };
            await _controller.CreateCourse(course);

            CourseMark mark = new CourseMark
            {
                Id = 1,
                Latitude = 0,
                Longitude = 0,
                Description = "Test",
                CourseId = 1
            };
            await _controller.CreateMark(mark);

            var retVal = await _dbContext.CourseMarks.FindAsync(1);
            Assert.NotNull(retVal);

            await _controller.UpdateCourseMark(1, new UpdateCourseMarkDTO { Description = "Update" });

            Assert.Equal("Update", retVal.Description);
        }

        [Fact]
        public async Task Delete_CourseMark()
        {
            Course course = new Course
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                courseMarks = null
            };
            await _controller.CreateCourse(course);

            CourseMark mark = new CourseMark
            {
                Id = 1,
                Latitude = 0,
                Longitude = 0,
                Description = "Test",
                CourseId = 1
            };
            await _controller.CreateMark(mark);

            var retVal = await _dbContext.CourseMarks.FindAsync(1);
            Assert.NotNull(retVal);

            await _controller.DeleteCourseMark(1);
            Assert.Null(await _dbContext.CourseMarks.FindAsync(1));
        }

    }
}
