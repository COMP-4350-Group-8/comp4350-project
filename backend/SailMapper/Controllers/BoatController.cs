using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;
using SailMapper.Data;

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
        /// 
        /// </summary>
        /// <returns>List of all boats</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoats()
        {
            List<Boat> boats = await boatService.GetBoats();
            return Ok(boats);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Id of created boat</returns>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>data of specified boat</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoat(int id)
        {
            Boat boat = await boatService.GetBoat(id);
            return Ok(boat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Http codes</returns>
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