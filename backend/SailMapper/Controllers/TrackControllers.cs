using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

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

        // to create a track
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

        // to get all tracks of a race
        [HttpGet("race/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRaceTracks(int id)
        {
            var tracks = await trackService.GetRaceTracks(id);
            return Ok(tracks);

        }

        // get a specific tracks
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


        //updating a track
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


        // getting gpx file
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