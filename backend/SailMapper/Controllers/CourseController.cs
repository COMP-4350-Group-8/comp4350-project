using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;
using SailMapper.Data;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/course")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService courseService;

        public CourseController(SailDBContext dbContext)
        {
            courseService = new CourseService(dbContext);
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
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {

             if (course == null)
             {
                 return BadRequest();
             }
             var id = await courseService.AddCourse(course);
             if (id != null)
             {
                return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
            }
            
            return Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all courses</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await courseService.GetCourses();
            return Ok(courses);
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
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await courseService.GetCourse(id);
            return Ok(course);
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
        public async Task<IActionResult> DeleteCourse(int id)
        {
            bool success = await courseService.DeleteCourse(id);
            return Ok(success);
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
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {

            if (course == null)
            {
                return BadRequest();
            }
            bool success = await courseService.UpdateCourse(course);
            if (id != null)
            {
                return Ok(id);
            }
            
            return Problem();
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
        public async Task<IActionResult> GetCourseMarks(int id)
        {
            var course = await courseService.GetCourseMarks(id);
            return Ok(course);
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
        public async Task<IActionResult> CreateMark([FromBody] CourseMark mark)
        {
            if (mark == null)
            {
                return BadRequest();
            }
            var id = await courseService.AddMark(mark);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetCourseMarks), new { id = mark.Id }, mark);
            }
            
            return Problem();
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
        public async Task<IActionResult> UpdateCourseMark(int id, [FromBody] CourseMark mark)
        {
            if (mark == null)
            {
                return BadRequest();
            }
            bool success = await courseService.UpdateCourseMark(mark);
            if (id != null)
            {
                return Ok(id);
            }
            
            return Problem();
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
        public async Task<IActionResult> DeleteCourseMark(int id, int markId)
        {
            bool success = await courseService.DeleteCourseMark(markId);
            return Ok(success);
        }



    }
}