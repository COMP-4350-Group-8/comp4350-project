using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/race")]
    public class RaceController : ControllerBase
    {
        private readonly RaceService raceService;

        public RaceController(SailDBContext dbContext)
        {
            raceService = new RaceService(dbContext);
        }

        /// <summary>
        /// Add a Race by sending a race object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Post /race
        /// {
        ///     "Course Id": string
        ///     "Start Time": DateTime
        ///     "End Time": DateTime
        ///     "Name": string
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Id of created race</returns>
        //[HttpPost("")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IResult> CreateRace(HttpRequestMessage request)
        //{
        //    //   if (request.Content != null)
        //    //   {
        //    var race = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
        //    if (race == null)
        //    {
        //        race = new Race();
        //        //return Results.BadRequest();
        //    }
        //    var id = await raceService.AddRace(race);
        //    if (id != null)
        //    {
        //        return Results.Created(id, race);
        //    }
        //    //  }
        //    return Results.Problem();
        //}

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRace([FromBody] Race race)
        {
            if (race == null)
            {
                return BadRequest("Invalid race data.");
            }

            var id = await raceService.AddRace(race);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetRace), new { id = race.Id }, race);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the race.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all races</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRaces()
        {
            var races = await raceService.GetRaces();
            return Ok(races);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Data for specified race</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRace(int id)
        {
            var race = await raceService.GetRace(id);
            return Ok(race);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http codes</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRace(int id)
        {
            bool success = await raceService.DeleteRace(id);
            return Ok(success);
        }

        /// <summary>
        /// 
        /// </summary>
        ///<remarks>
        /// send a partially populated Race object, all populated fields will be updated to the given data
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Http codes</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRace(int id, [FromBody] Race race)
        {
            
            bool success = await raceService.UpdateRace(race);
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
        /// <returns>List of all boat objects in the specified race</returns>
        [HttpGet("{id}/participants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParticipants(int id)
        {
            List<Boat> boats = await raceService.GetParticipants(id);
            if (boats == null)
            {
                return NotFound();
            }
            return Ok<List<Boat>>(boats);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boat"></param>
        /// <returns>Http codes</returns>
        [HttpPut("{id}/participant/{boat}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddParticipant(int id, int boat)
        {
            bool success = await raceService.AddParticipant(id, boat);
            if (success)
            {
                return Ok(id);
            }
            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boat"></param>
        /// <returns>Http codes</returns>
        [HttpDelete("{id}/participant/{boat}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveParticipant(int id, int boat)
        {
            bool success = await raceService.RemoveParticipant(id, boat);
            if (success)
            {
                return Ok(id);
            }
            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result object for each boat in the race</returns>
        [HttpGet("{id}/results")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetResults(int id)
        {
            List<Result> result = await raceService.GetResults(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result object for each boat in the race</returns>
        [HttpGet("{id}/calculate/{baseBoat}/{B}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CalculateResults(int id, int baseBoat, int B)
        {
            List<Result> result = await raceService.CalculateResults(id, baseBoat, B);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

    }
}