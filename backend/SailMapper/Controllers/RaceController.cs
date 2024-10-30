using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;

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
        /// Creates a new Race by sending a Race object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /race
        /// 
        /// {  
        /// 
        ///     "id": int,                  // id of the race
        ///     
        ///     "startTime": dateTime,      // UTC start time of the race
        ///     
        ///     "endTime": dateTime,        // UTC end time of the race
        ///     
        ///     "name": string,              // Name of the race
        ///     
        ///     "participants": [ Boat ],    // can be many Boats. Please refer to POST Boat
        ///     
        ///     "courses": [ Course ],       // can be many Courses. Please refer to POST Course
        ///     
        ///     "results": [                // can be many Results
        ///         
        ///         {
        ///         
        ///         "id": int,              // id of the Result
        ///
        ///         "boatId": int,          // id of the Boat it is attached to
        ///
        ///         "raceId": int,          // id of the Race it is attached to
        ///         
        ///         "finishPosition": int,  // at what place did the boat finish the race
        ///
        ///         "elapsedTime": string,  // time it took to complete the race
        ///         
        ///         "correctedTime": string,// time after the penalties
        ///         
        ///         "rating": int,          // rating of the boat
        ///         
        ///         "points": int,          // how many points did the boat get
        ///         
        ///         "finishType": string    // disqualification (DQ), do not finish (DNF), normal 
        ///         
        ///         } 
        ///         
        ///     ],
        ///     
        ///     "tracks": [ Track ],         // can be many Tracks. Please refer to POST Track
        ///     
        ///     "regattaId": int            // id of the Regatta the race belongs to
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Race created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
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
        /// Returns a list of all races
        /// </summary>
        /// <response code="200">list of all races returned successfully.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRaces()
        {
            var races = await raceService.GetRaces();
            return Ok(races);
        }

        /// <summary>
        /// Returns a specific Race 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Race returned successfully.</response>
        /// <response code="404">Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRace(int id)
        {
            var race = await raceService.GetRace(id);
            if (race == null)
            {
                return NotFound();
            }
            return Ok(race);
        }

        /// <summary>
        /// Deletes a specific Race 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Race deleted successfully.</response>
        /// <response code="404">Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
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
        /// Update a Race by sending a Race object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /race/{id}
        /// 
        /// {  
        /// 
        ///     "id": int,                  // id of the race
        ///     
        ///     "startTime": dateTime,      // UTC start time of the race
        ///     
        ///     "endTime": dateTime,        // UTC end time of the race
        ///     
        ///     "name": string,              // Name of the race
        ///     
        ///     "participants": [ Boat ],    // can be many Boats. Please refer to POST Boat
        ///     
        ///     "courses": [ Course ],       // can be many Courses. Please refer to POST Course
        ///     
        ///     "results": [                // can be many Results
        ///         
        ///         {
        ///         
        ///         "id": int,              // id of the Result
        ///
        ///         "boatId": int,          // id of the Boat it is attached to
        ///
        ///         "raceId": int,          // id of the Race it is attached to
        ///         
        ///         "finishPosition": int,  // at what place did the boat finish the race
        ///
        ///         "elapsedTime": string,  // time it took to complete the race
        ///         
        ///         "correctedTime": string,// time after the penalties
        ///         
        ///         "rating": int,          // rating of the boat
        ///         
        ///         "points": int,          // how many points did the boat get
        ///         
        ///         "finishType": string    // disqualification (DQ), do not finish (DNF), normal 
        ///         
        ///         } 
        ///         
        ///     ],
        ///     
        ///     "tracks": [ Track ],         // can be many Tracks. Please refer to POST Track
        ///     
        ///     "regattaId": int            // id of the Regatta the race belongs to
        ///     
        /// }
        /// </remarks>
        /// <response code="200">Race updated successfully.</response>
        /// <response code="404">Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Returns a list of all Boat objects in the specific race 
        /// </summary>
        /// <response code="200">list of Boat objects returned successfully.</response>
        /// <response code="404">Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
            return Ok(boats);
        }

        /// <summary>
        /// Add a participant (Boat) id to a race id
        /// </summary>
        /// <response code="202">The request was successfull.</response>
        /// <response code="404">Race or Boat not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        /// <param name="boat"></param>
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
        /// Delete a participant (Boat) id from a race id
        /// </summary>
        /// <response code="202">The request was successfull.</response>
        /// <response code="404">Race or Boat not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        /// <param name="boat"></param>
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
        /// Returns a Result object from a specific race
        /// </summary>
        /// <response code="202">The request was successfull.</response>
        /// <response code="404">Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Returns a Result difference for a boat(B) based on boat(baseBoat) from a specific race(id)  
        /// </summary>
        /// <response code="202">The request was successfull.</response>
        /// <response code="404">Race or Boat not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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