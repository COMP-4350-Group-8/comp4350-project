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

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetBoats()
        {
            Boat[] boats = await boatService.GetBoats();
            return Results.Ok(boats);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateBoat(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var boat = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetBoat(string id)
        {
            Boat boat = await boatService.GetBoat(id);
            return Results.Ok(boat);
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> UpdateBoat(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var boat = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
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
