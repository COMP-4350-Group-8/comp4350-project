using Microsoft.AspNetCore.Mvc;
using SailMapper.Data;
using SailMapper.Classes;

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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200Ok)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRaceTracks(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.NotFound();
                }
                var id = await trackService.getRaceTracks(track);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        // to get all tracks of a boat
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200Ok)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetBoatTracks(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.NotFound();
                }
                var id = await trackService.getBoatTracks(track);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        // get a specific tracks
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200Ok)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetTrack(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.NotFound();
                }
                var id = await trackService.getTrack(track);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }


        //updating a track
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200Ok)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateTrack(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.NotFound();
                }
                var id = await trackService.updateTrack(track);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }


        // getting gpx file
        [HttpGet("{id}/gpx")]
        [ProducesResponseType(StatusCodes.Status200Ok)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetTrackGPX(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var track = await JsonSerializer.DeserializeAsync<Track>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (track == null)
                {
                    return Results.NotFound();
                }
                var id = await trackService.getGPX(track);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

    }
}
