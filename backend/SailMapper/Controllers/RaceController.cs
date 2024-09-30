using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateRace(HttpRequestMessage request, string json)
        {
            var result = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            if (result == null)
            {
                return Results.BadRequest();
            }
            var id = await raceService.AddRace(result);
            if (id != null)
            {
                return Results.Created(id, result); 
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
    }
}
