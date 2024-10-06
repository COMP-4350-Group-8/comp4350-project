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

        /// <summary>
        ///Creates a regatta 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>The id of the created regatta</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all regattas</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRegattas()
        {
            var regattas = await regattaService.GetRegattas();
            return Results.Ok(regattas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All data for the specified regatta</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRegatta(string id)
        {
            var regatta = await regattaService.GetRegatta(id);
            return Results.Ok(regatta);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Http codes</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateRegatta(string id, HttpRequestMessage request)
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

            bool success = await regattaService.UpdateRegatta(id, regatta);
            if (success)
            {
                return Results.Ok();
            }

            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http codes</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> DeleteRegatta(string id)
        {
            bool success = await regattaService.DeleteRegatta(id);
            return Results.Ok(success);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="raceId"></param>
        /// <returns>Http codes</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="raceId"></param>
        /// <returns>Http codes</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns table of results</returns>
        [HttpGet("{id}/results")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRegattaResults(string id)
        {
            var results = await regattaService.GetRegattaResults(id);
            return Results.Ok(results);

        }



    }
}