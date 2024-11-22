using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

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
        /// Creates a new Course by sending a Course object with CourseMarks
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /course
        /// 
        /// {  
        /// 
        ///     "id": int,                      // id of the course
        ///     
        ///     "name": string,                 // Name of the course
        ///     
        ///     "description": string,          // Some notes about the course
        ///     
        ///     "courseMarks": [                // can be many CourseMarks
        ///        
        ///         {
        ///         
        ///             "id": int,              // id of the courseMark
        ///             
        ///             "latitude": float,      // latitude of the courseMark
        ///             
        ///             "longitude": float,     // longitude of the courseMark
        ///             
        ///             "description": string,  // some info about the courseMark
        ///             
        ///             "rounding": boolean,    // true if Sailers need to go clockwise around the courseMark and false is counter-clockwise (port for counter-clockwise, starboard for clockwise)
        ///             
        ///             "isStartLine": boolean, // true id the courseMark is one of the points of start line
        ///             
        ///             "gateId": id,           // a reference to another CourseMark that makes up a gate, if not gate - skip
        ///             
        ///             "courseId": int         // id of the Course the courseMark belongs to (omit this line, since MySql detects it)
        ///             
        ///         }]
        ///         
        /// }
        /// </remarks>
        /// <response code="201">Course created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
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
        /// Returns a list of all courses and courseMarks
        /// </summary>
        /// <response code="200">list of all courses returned successfully.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await courseService.GetCourses();
            return Ok(courses);
        }

        /// <summary>
        /// Returns a specific course
        /// </summary>
        /// <response code="200">Course returned successfully.</response>
        /// <response code="404">Course not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Deletes a specific course
        /// </summary>
        /// <response code="200">Course deleted successfully.</response>
        /// <response code="404">Course not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Updates a Course by sending a Course object with CourseMarks
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /course/{id}
        /// 
        /// {  
        /// 
        ///     "id": int,                      // id of the course
        ///     
        ///     "name": string,                 // Name of the course
        ///     
        ///     "description": string,          // Some notes about the course
        ///     
        ///     "courseMarks": [                // can be many CourseMarks
        ///        
        ///         {
        ///         
        ///             "id": int,              // id of the courseMark
        ///             
        ///             "latitude": float,      // latitude of the courseMark
        ///             
        ///             "longitude": float,     // longitude of the courseMark
        ///             
        ///             "description": string,  // some info about the courseMark
        ///             
        ///             "rounding": boolean,    // true if Sailers need to go clockwise around the courseMark and false is counter-clockwise (port for counter-clockwise, starboard for clockwise)
        ///             
        ///             "isStartLine": boolean, // true id the courseMark is one of the points of start line
        ///             
        ///             "gateId": id,           // a reference to another CourseMark that makes up a gate, if not gate - skip
        ///             
        ///             "courseId": int         // id of the Course the courseMark belongs to (omit this line, since MySql detects it)
        ///             
        ///         }]
        ///         
        /// }
        /// </remarks>
        /// <response code="200">Course updated successfully.</response>
        /// <response code="404">Course not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Returns a list of courseMarks from a specific course
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="404">Course not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Creates CourseMarks for a specific Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /marks
        /// 
        /// {  
        ///         
        ///     "id": int,              // id of the courseMark
        ///             
        ///     "latitude": float,      // latitude of the courseMark
        ///             
        ///     "longitude": float,     // longitude of the courseMark
        ///             
        ///     "description": string,  // some info about the courseMark
        ///             
        ///     "rounding": boolean,    // true if Sailers need to go clockwise around the courseMark and false is counter-clockwise (port for counter-clockwise, starboard for clockwise)
        ///             
        ///     "isStartLine": boolean, // true id the courseMark is one of the points of start line
        ///             
        ///     "gateId": id,           // a reference to another CourseMark that makes up a gate, if not gate - skip
        ///             
        ///     "courseId": int         // id of the Course the courseMark belongs to (omit this line, since MySql detects it)
        ///     
        /// }
        /// </remarks>
        /// <response code="202">CourseMark created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="404">Course not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("marks")]
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
        /// Updates CourseMarks for a specific Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /marks/{id}
        /// 
        /// {  
        ///         
        ///     "id": int,              // id of the courseMark
        ///             
        ///     "latitude": float,      // latitude of the courseMark
        ///             
        ///     "longitude": float,     // longitude of the courseMark
        ///             
        ///     "description": string,  // some info about the courseMark
        ///             
        ///     "rounding": boolean,    // true if Sailers need to go clockwise around the courseMark and false is counter-clockwise (port for counter-clockwise, starboard for clockwise)
        ///             
        ///     "isStartLine": boolean, // true id the courseMark is one of the points of start line
        ///             
        ///     "gateId": id,           // a reference to another CourseMark that makes up a gate, if not gate - skip
        ///             
        ///     "courseId": int         // id of the Course the courseMark belongs to (omit this line, since MySql detects it)
        ///     
        /// }
        /// </remarks>
        /// <response code="202">CourseMark updated successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="404">Course not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpPut("marks/{id}")]
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
        /// Deletes a courseMark
        /// </summary>
        /// <response code="200">CourseMark deleted successfully.</response>
        /// <response code="404">CourseMark not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpDelete("marks/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCourseMark(int id)
        {
            bool success = await courseService.DeleteCourseMark(id);
            return Ok(success);
        }



    }
}