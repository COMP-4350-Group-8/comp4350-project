using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/course")]
    public class CourseController
    {
        private readonly CourseService courseService;

        public CourseController()
        {
            courseService = new CourseService();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateCourse(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var course = await JsonSerializer.DeserializeAsync<Course>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (course == null)
                {
                    return Results.BadRequest();
                }
                var id = await courseService.AddCourse(course);
                if (id != null)
                {
                    return Results.Created(id, course);
                }
            }
            return Results.Problem();
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetCourses()
        {
            var courses = await courseService.GetCourses();
            return Results.Ok(courses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetCourse(string id)
        {
            var course = await courseService.GetCourse(id);
            return Results.Ok(course);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteCourse(string id)
        {
            bool success = await courseService.DeleteCourse(id);
            return Results.Ok(success);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateCourse(string id, HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                var course = await JsonSerializer.DeserializeAsync<Course>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (course == null)
                {
                    return Results.BadRequest();
                }
                bool success = await courseService.updateCourse(id, course);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        [HttpGet("{id}/marks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetCourseMarks(string id)
        {
            var course = await courseService.GetCourseMarks(id);
            return Results.Ok(course);
        }

        [HttpPost("{id}/marks")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         public async Task<IResult> CreateMark(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var mark = await JsonSerializer.DeserializeAsync<Course>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (mark == null)
                {
                    return Results.BadRequest();
                }
                var id = await courseService.AddMark(mark);
                if (id != null)
                {
                    return Results.Created(id, mark);
                }
            }
            return Results.Problem();
        }

        [HttpPut("{id}/marks/{markId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateCourseMark(string id, HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                var mark= await JsonSerializer.DeserializeAsync<Course>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (mark == null)
                {
                    return Results.BadRequest();
                }
                bool success = await courseService.UpdateCourseMark(mark);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        [HttpDelete("{id}/marks/{markId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteCourseMark(string id, string markId)
        {
            bool success = await courseService.DeleteCourseMark(id, markId);
            return Results.Ok(success);
        }



    }
}
