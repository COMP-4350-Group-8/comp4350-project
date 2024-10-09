using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/regatta")]
    public class RegattaController : ControllerBase
    {
        private readonly RegattaService regattaService;

        public RegattaController(SailDBContext dbContext)
        {
            regattaService = new RegattaService(dbContext);
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
        public async Task<IActionResult> CreateRegatta([FromBody] Regatta regatta)
        {
            if (regatta == null)
            {
                return BadRequest();
            }

            var id = await regattaService.AddRegatta(regatta);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetRegatta), new { id = regatta.Id }, regatta);
            }

            return Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all regattas</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRegattas()
        {
            var regattas = await regattaService.GetRegattas();
            return Ok(regattas);
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
        public async Task<IActionResult> GetRegatta(int id)
        {
            var regatta = await regattaService.GetRegatta(id);
            return Ok(regatta);

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
        public async Task<IActionResult> UpdateRegatta(int id, [FromBody] Regatta regatta)
        {
            if (regatta == null)
            {
                return BadRequest();
            }

            bool success = await regattaService.UpdateRegatta(regatta);
            if (success)
            {
                return Ok();
            }

            return Problem();
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
        public async Task<IActionResult> DeleteRegatta(int id)
        {
            bool success = await regattaService.DeleteRegatta(id);
            return Ok(success);
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
        public async Task<IActionResult> AddRace(int id, int raceId)
        {
            bool success = await regattaService.AddRace(id, raceId);
            if (success)
            {
                return Ok(success);
            }
            else
            {
                return Problem(raceId.ToString());
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
        public async Task<IActionResult> DeleteRegatta(int id, int raceId)
        {
            bool success = await regattaService.RemoveRace(id, raceId);
            if (success)
            {
                return Ok(success);
            }
            else
            {
                return Problem(raceId.ToString());
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
        public async Task<IActionResult> GetRegattaResults(int id)
        {
            var results = await regattaService.GetRegattaResults(id);
            return Ok(results);

        }



    }
}