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

            Assert.IsType<List<Race>>(result);
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
            var retVal = await _dbContext.Courses.FindAsync(1);
            Assert.NotNull(retVal);
            Assert.Equal(1, retVal.Id);
            Assert.Equal("Test", retVal.Name);
            Assert.Equal("Test", retVal.Description);

            await _controller.UpdateCourse(1, new UpdateCourseDTO { Name = "Update" });

            Assert.Equal("Update", retVal.Name);
        }
    }
}
