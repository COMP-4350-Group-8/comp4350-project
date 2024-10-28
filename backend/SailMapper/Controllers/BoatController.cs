using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/boat")]
    public class BoatController : ControllerBase
    {

        private readonly BoatService boatService;

        public BoatController(SailDBContext dbContext)
        {
            boatService = new BoatService(dbContext);
        }

        /// <summary>
        /// Returns a list of all boats
        /// </summary>
        /// <response code="200">The request was successfull.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoats()
        {
            List<Boat> boats = await boatService.GetBoats();
            return Ok(boats);
        }

        /// <summary>
        /// Creates a new Boat by sending a Boat object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /boat
        /// 
        /// {  
        /// 
        ///     "id": int,              // ID of the boat (put any number, since Database will assign the id automatically)
        ///        
        ///     "name": string         // Name of the boat
        /// 
        /// }
        /// </remarks>
        /// <response code="201">Boat created successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBoat([FromBody] Boat boat)
        {

            if (boat == null)
            {
                return BadRequest();
            }
            var id = await boatService.AddBoat(boat);
            if (id != null)
            {
                return Created(id, boat);
            }

            return Problem();
        }

        /// <summary>
        /// Returns a specific boat
        /// </summary>
        /// <response code="200">Boat returned successfully.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoat(int id)
        {
            Boat boat = await boatService.GetBoat(id);
            return Ok(boat);
        }

        /// <summary>
        /// Updates a specific Boat
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /boat/{id}
        /// 
        /// {  
        /// 
        ///     "id": int,              // ID of the boat (put any number, since Database will assign the id automatically)
        ///        
        ///     "name": string         // Name of the boat
        /// 
        /// }
        /// </remarks>
        /// <response code="201">Boat updated successfully.</response>
        /// <response code="400">If any of the required fields are missing or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBoat([FromBody] Boat boat)
        {

            if (boat == null)
            {
                return BadRequest();
            }
            bool success = await boatService.UpdateBoat(boat);
            if (success)
            {
                return Ok();
            }

            return Problem();
        }
    }
}