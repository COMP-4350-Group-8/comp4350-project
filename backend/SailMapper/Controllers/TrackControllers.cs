using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text.Json;
using System.Text;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/track")]
    public class TrackControllers
    {
        private readonly TrackService trackService;
        public TrackControllers()
        {
            trackService = new TrackService();
        }

        // to create a track
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateTrack(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.BadRequest();
                }
                var id = await trackService.AddTrack(track);
                if (id != null)
                {
                    return Results.Created(id, track);
                }
            }
            return Results.Problem();
        }

        // to get all tracks of a race
        [HttpGet("race/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRaceTracks(string id)
        {
                var tracks= await trackService.GetRaceTracks(id);
                return Results.Ok(tracks);

        }

        // get a specific tracks
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetTrack(string id)
        {
                var track = await trackService.GetTrack(id);
                if (track == null)
                {
                    return Results.Problem();
                }
                return Results.Ok(id);
        }


        //updating a track
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateTrack(string id, HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.BadRequest();
                }
                var result = await trackService.UpdateTrack(track);
                if (result == null)
                {
                    return Results.NotFound(id);
                }
                return Results.Ok(id);
            }
            return Results.Problem();
        }


        // getting gpx file
        [HttpGet("{id}/gpx")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetTrackGPX(string id)
        {
                var points = await trackService.GetGPX(id);
                if (points == null)
                {
                    return Results.NotFound(id);
                }
                return Results.Ok(points);
        }

    }
}
