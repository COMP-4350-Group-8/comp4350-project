using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/boat")]
    public class BoatController : ControllerBase
    {

        private readonly BoatService boatService;

        public BoatController(SailDBContext dbContext)
        {
            boatService = new BoatService(dbContext);
        }

        /// <summary>
        /// Returns a list of all boats
        /// </summary>
        /// <response code="200">The request was successfull.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoats()
        {
            List<Boat> boats = await boatService.GetBoats();
            return Ok(boats);
        }

        /// <summary>
        /// Creates a new Boat by sending a Boat object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /boat
        /// 
        /// {  
        /// 
        ///     "id": int,                      // id of the Boat (put any number, since Database will assign the id automatically)
        ///        
        ///     "name": string,                 // name of the boat
        ///         
        ///     "class": string,                // in what class boat competes
        ///     
        ///     "sailNimber": string,           // Sail number
        ///     
        ///     "skipper": string,              // name of the skipper
        ///     
        ///     "ratingId": int,                // id of the Rating
        ///     
        ///     "rating": {                     // can be one rating per boat 
        ///     
        ///         "id": int,                  // id of the Rating
        ///         
        ///         "baseRating": int,          // rating of the boat
        ///         
        ///         "spinnakerAdjustment": int, // spinnaker adjustment configurations
        ///         
        ///         "adjustment": int,          
        ///         
        ///         "currentRating": int,       // current rating
        ///         
        ///         "boats": [ Boat ]           // can be many Boats
        ///         
        ///     },
        ///     
        ///     "results": [                    // can be many Results
        ///         
        ///         {
        ///         
        ///         "id": int,                  // id of the Result
        /// 
        ///         "boatId": int,              // id of the Boat it is attached to
        ///
        ///         "raceId": int,              // id of the Race it is attached to
        ///         
        ///         "finishPosition": int,      // at what place did the boat finish the race
        ///
        ///         "elapsedTime": string,      // time it took to complete the race
        ///         
        ///         "correctedTime": string,    // time after the penalties
        ///         
        ///         "rating": int,              // rating of the boat
        ///         
        ///         "points": int,              // how many points did the boat get
        ///         
        ///         "finishType": string        // disqualification (DQ), do not finish (DNF), normal 
        ///         
        ///         } 
        ///         
        ///     ],
        ///     
        ///     "tracks": [ Track ],            // can be many Tracks. Please refer to POST Track
        ///     
        ///     "races": [ Race ]               // can be many Races. Please refer to POST Race
        /// 
        /// }
        /// </remarks>
        /// <response code="201">Boat created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBoat([FromBody] Boat boat)
        {

            if (boat == null)
            {
                return BadRequest();
            }
            var id = await boatService.AddBoat(boat);
            if (id != null)
            {
                return Created(id, boat);
            }

            return Problem();
        }

        /// <summary>
        /// Returns a specific boat
        /// </summary>
        /// <response code="200">Boat returned successfully.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoat(int id)
        {
            Boat boat = await boatService.GetBoat(id);
            return Ok(boat);
        }

        /// <summary>
        /// Updates a specific Boat
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /boat/{id}
        /// 
        /// {  
        /// 
        ///     "id": int,                      // id of the Boat (put any number, since Database will assign the id automatically)
        ///        
        ///     "name": string,                 // name of the boat
        ///         
        ///     "class": string,                // in what class boat competes
        ///     
        ///     "sailNimber": string,           // Sail number
        ///     
        ///     "skipper": string,              // name of the skipper
        ///     
        ///     "ratingId": int,                // id of the Rating
        ///     
        ///     "rating": {                     // can be one rating per boat 
        ///     
        ///         "id": int,                  // id of the Rating
        ///         
        ///         "baseRating": int,          // rating of the boat
        ///         
        ///         "spinnakerAdjustment": int, // spinnaker adjustment configurations
        ///         
        ///         "adjustment": int,          
        ///         
        ///         "currentRating": int,       // current rating
        ///         
        ///         "boats": [ Boat ]           // can be many Boats
        ///         
        ///     },
        ///     
        ///     "results": [                    // can be many Results
        ///         
        ///         {
        ///         
        ///         "id": int,                  // id of the Result
        /// 
        ///         "boatId": int,              // id of the Boat it is attached to
        ///
        ///         "raceId": int,              // id of the Race it is attached to
        ///         
        ///         "finishPosition": int,      // at what place did the boat finish the race
        ///
        ///         "elapsedTime": string,      // time it took to complete the race
        ///         
        ///         "correctedTime": string,    // time after the penalties
        ///         
        ///         "rating": int,              // rating of the boat
        ///         
        ///         "points": int,              // how many points did the boat get
        ///         
        ///         "finishType": string        // disqualification (DQ), do not finish (DNF), normal 
        ///         
        ///         } 
        ///         
        ///     ],
        ///     
        ///     "tracks": [ Track ],            // can be many Tracks. Please refer to POST Track
        ///     
        ///     "races": [ Race ]               // can be many Races. Please refer to POST Race
        /// 
        /// }
        /// </remarks>
        /// <response code="201">Boat updated successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBoat([FromBody] Boat boat)
        {

            if (boat == null)
            {
                return BadRequest();
            }
            bool success = await boatService.UpdateBoat(boat);
            if (success)
            {
                return Ok();
            }

            return Problem();
        }
    }
}