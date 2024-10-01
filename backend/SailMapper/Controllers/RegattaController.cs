using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/regatta")]
    public class RegattaController
    {
        private readonly RegattaService regattaService;

        public RegattaController()
        {
            regattaService = new RegattaService();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateRegatta(HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                return Results.BadRequest();
            }

            var regatta = await JsonSerializer.DeserializeAsync<Regatta>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
            if (regatta == null)
            {
                return Results.BadRequest();
            }

            var id = await regattaService.AddRegatta(regatta);
            if (id != null)
            {
                return Results.Created(id, regatta);
            }

            return Results.Problem();
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRegattas()
        {
            var regattas = await regattaService.GetRegattas();
            return Results.Ok(regattas);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRegatta(string id)
        {
            var regatta = await regattaService.GetRegatta(id);
            return Results.Ok(regatta);

        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateRegatta(string id)
        {
            if (request != null && request.Content != null)
            {
                return Results.BadRequest();
            }

            var regatta = await JsonSerializer.DeserializeAsync<Regatta>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
            if (regatta == null)
            {
                return Results.BadRequest();
            }

            bool success = await regattaService.UpdateRegatta(regatta);
            if (success)
            {
                return Results.Ok();
            }

            return Results.Problem();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> DeleteRegatta(string id)
        {
            bool success = await regattaService.DeleteRegatta(id);
            return Results.Ok(success);
        }

        [HttpPut("{id}/races/{raceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> AddRace(string id, string raceId)
        {
            bool success = await regattaService.AddRace(id, raceId);
            if (success)
            {
                return Results.Ok(success);
            }
            else
            {
                return Results.Problem(raceId);
            }
        }



        [HttpDelete("{id}/races/{raceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> DeleteRegatta(string id, string raceId)
        {
            bool success = await regattaService.RemoveRace(id, raceId);
            if (success)
            {
                return Results.Ok(success);
            }
            else
            {
                return Results.Problem(raceId);
            }
        }



    }
}
