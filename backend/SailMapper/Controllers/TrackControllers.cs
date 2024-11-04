using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/track")]
    public class TrackControllers : ControllerBase
    {
        private readonly TrackService trackService;
        public TrackControllers(SailDBContext dbContext)
        {
            trackService = new TrackService(dbContext);
        }

        /// <summary>
        /// Creates a track
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /track
        /// 
        /// {  
        /// 
        ///     "id": int,                  // id of the track
        ///     
        ///     "boatId": int,              // id of the boat track belongs to
        ///     
        ///     "raceId": int,              // id of the race the boat was tracked
        ///     
        ///     "started": dateTime,        // UTC start time of the race 
        ///     
        ///     "finished": dateTime,       // UTC end time of the race
        ///     
        ///     "distance": float,          // distance covered by the boat
        ///     
        ///     "gpxData": string,          // XML file from the GPS tracker
        ///     
        ///     "currentRating": int        // current rating of the boat
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Track created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTrack([FromBody] Track track)
        {
            if (track == null)
            {
                return BadRequest();
            }
            var id = await trackService.AddTrack(track);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetTrack), new { id = track.Id }, track);
            }

            return Problem();
        }

        /// <summary>
        /// Gets all the tracks for the specific race
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpGet("race/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRaceTracks(int id)
        {
            var tracks = await trackService.GetRaceTracks(id);
            return Ok(tracks);

        }

        /// <summary>
        /// Gets a specific track
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="404">Track not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrack(int id)
        {
            var track = await trackService.GetTrack(id);
            if (track == null)
            {
                return Problem();
            }
            return Ok(id);
        }


        /// <summary>
        /// Updates a track
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /track/{id}
        /// 
        /// {  
        /// 
        ///     "id": int,                  // id of the track
        ///     
        ///     "boatId": int,              // id of the boat track belongs to
        ///     
        ///     "raceId": int,              // id of the race the boat was tracked
        ///     
        ///     "started": dateTime,        // UTC start time of the race 
        ///     
        ///     "finished": dateTime,       // UTC end time of the race
        ///     
        ///     "distance": float,          // distance covered by the boat
        ///     
        ///     "gpxData": string,          // XML file from the GPS tracker
        ///     
        ///     "currentRating": int        // current rating of the boat
        ///     
        /// }
        /// </remarks>
        /// <response code="200">Track updated successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="404">Track not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTrack(int id, [FromBody] Track track)
        {

            if (track == null)
            {
                return BadRequest();
            }
            var result = await trackService.UpdateTrack(track);
            if (result == null)
            {
                return NotFound(id);
            }
            else
            {
                return Ok(id);
            }

            return Problem();
        }


        /// <summary>
        /// Gets a GPX file
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="404">Track not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpGet("{id}/gpx")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrackGPX(int id)
        {
            var points = await trackService.GetGPX(id);
            if (points == null)
            {
                return NotFound(id);
            }
            return Ok(points);
        }

    }
}