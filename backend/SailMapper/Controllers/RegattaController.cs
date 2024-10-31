using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;

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
        /// Creates a regatta 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST regatta
        /// 
        /// {  
        /// 
        ///     "id": int,                  // id of the regatta
        ///
        ///     "name": string,             // Name of the regatta
        ///     
        ///     "description": string,      // details of the regatta
        ///     
        ///     "races": [ Race ]           // can be many Races. Please refer to POST Race
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Regatta created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
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
        /// List of all regattas
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRegattas()
        {
            var regattas = await regattaService.GetRegattas();
            return Ok(regattas);
        }

        /// <summary>
        /// Get all data for the specified regatta
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="404">Reggatta not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Update specific regatta
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /regatta/{id}
        /// 
        /// {  
        /// 
        ///     "id": int,                  // id of the regatta
        ///
        ///     "name": string,             // Name of the regatta
        ///     
        ///     "description": string,      // details of the regatta
        ///     
        ///     "races": [ Race ]           // can be many Races. Please refer to POST Race
        ///     
        /// }
        /// </remarks>
        /// <response code="200">Regatta updated successfully.</response>
        /// <response code="404">Regatta not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Delete regatta
        /// </summary>
        /// <response code="200">Regatta deleted successfully.</response>
        /// <response code="404">Regatta not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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
        /// Adding a race to regatta
        /// </summary>
        /// <response code="200">Race added to regatta successfully.</response>
        /// <response code="404">Regatta or Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        /// <param name="raceId"></param>
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
        /// Deleting a race from regatta
        /// </summary>
        /// <response code="200">Race deleted from regatta successfully.</response>
        /// <response code="404">Regatta or Race not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        /// <param name="raceId"></param>
        [HttpDelete("{id}/races/{raceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRace(int id, int raceId)
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
        /// Returns a table of results
        /// </summary>
        /// <response code="200">Action successfull.</response>
        /// <response code="404">Regatta not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
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