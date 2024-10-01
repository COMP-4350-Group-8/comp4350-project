using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/race")]
    public class RaceController
    {
        private static readonly string[] FinishTypes = new[]
        {
             "DNS", "DNF", "RET", "DSQ", "OCS", "BFD", "DNC", "DGM", "RDG", "SCP", "ZFP", "UFD", "TLE", "NSC"
        };

        private readonly RaceService raceService;

        public RaceController()
        {
            raceService = new RaceService();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateRace(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var race = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (race == null)
                {
                    return Results.BadRequest();
                }
                var id = await raceService.AddRace(race);
                if (id != null)
                {
                    return Results.Created(id, race);
                }
            }
            return Results.Problem();
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRaces()
        {
            var races = await raceService.GetRaces();
            return Results.Ok(races);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetRace(string id)
        {
            var race = await raceService.GetRace(id);
            return Results.Ok(race);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteRace(string id)
        {
            bool success = await raceService.DeleteRace(id);
            return Results.Ok(success);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateRace(string id, HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                var race = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(json)));
                if (race == null)
                {
                    return Results.BadRequest();
                }
                bool success = await raceService.updateRace(id, race);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        [HttpGet("{id}/participants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetParticipants(string id)
        {
            Boat[] boats = await raceService.getParticipants(id);
            if (boats == null)
            {
                return Results.NotFound();
            }
            return Results.Ok<Boat[]>(boats);
        }


        [HttpPut("{id}/participant/{boat}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> AddParticipant(string id, string boat)
        {
           bool success = await raceService.AddParticipant(id, boat);
            if (success)
            {
                return Results.Ok(id);
            }
            return Results.NotFound();
        }

        [HttpDelete("{id}/participant/{boat}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> RemoveParticipant(string id, string boat)
        {
           bool success = await raceService.RemoveParticipant(id, boat);
            if (success)
            {
                return Results.Ok(id);
            }
            return Results.NotFound();
        }

        [HttpGet("{id}/results")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetResults(string id)
        {
            Result result = await raceService.GetResults(id);
            if(result == null)
                return Results.NotFound();
            return Results.Ok(result);
        }


    }
}
