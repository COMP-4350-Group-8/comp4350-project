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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                    return Results.Created(id.ToString(), course);
                }
            }
            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all courses</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetCourses()
        {
            var courses = await courseService.GetCourses();
            return Results.Ok(courses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> data for a specific course</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetCourse(int id)
        {
            var course = await courseService.GetCourse(id);
            return Results.Ok(course);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Http codes</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteCourse(int id)
        {
            bool success = await courseService.DeleteCourse(id);
            return Results.Ok(success);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns> http codes</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateCourse(int id, HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                var course = await JsonSerializer.DeserializeAsync<Course>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (course == null)
                {
                    return Results.BadRequest();
                }
                bool success = await courseService.UpdateCourse(course);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list of course marks</returns>
        [HttpGet("{id}/marks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetCourseMarks(int id)
        {
            var course = await courseService.GetCourseMarks(id);
            return Results.Ok(course);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Http codes</returns>
        [HttpPost("{id}/marks")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateMark(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                CourseMark mark = await JsonSerializer.DeserializeAsync<CourseMark>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (mark == null)
                {
                    return Results.BadRequest();
                }
                var id = await courseService.AddMark(mark);
                if (id != null)
                {
                    return Results.Created(id.ToString(), mark);
                }
            }
            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Http codes</returns>
        [HttpPut("{id}/marks/{markId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateCourseMark(int id, HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                var mark = await JsonSerializer.DeserializeAsync<CourseMark>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="markId"></param>
        /// <returns>Http codes</returns>
        [HttpDelete("{id}/marks/{markId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteCourseMark(int id)
        {
            bool success = await courseService.DeleteCourseMark(id);
            return Results.Ok(success);
        }



    }
}