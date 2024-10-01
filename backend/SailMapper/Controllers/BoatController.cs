using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/boat")]
    public class BoatController
    {

        private readonly BoatService boatService;

        public BoatController()
        {
            boatService = new BoatService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all boats</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetBoats()
        {
            Boat[] boats = await boatService.GetBoats();
            return Results.Ok(boats);
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
        public async Task<IResult> CreateBoat(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                Boat boat = await JsonSerializer.DeserializeAsync<Boat>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (boat == null)
                {
                    return Results.BadRequest();
                }
                var id = await boatService.AddBoat(boat);
                if (id != null)
                {
                    return Results.Created(id, boat);
                }
            }
            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>data of specified boat</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetBoat(string id)
        {
            Boat boat = await boatService.GetBoat(id);
            return Results.Ok(boat);
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
        public async Task<IResult> UpdateBoat(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                Boat boat = await JsonSerializer.DeserializeAsync<Boat>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (boat == null)
                {
                    return Results.BadRequest();
                }
                bool success = await boatService.UpdateBoat(boat);
                if (success)
                {
                    return Results.Ok();
                }
            }
            return Results.Problem();
        }
    }
}
