using Microsoft.AspNetCore.Mvc;
using SailMapper.Data;
using SailMapper.Classes;
using Microsoft.EntityFrameworkCore;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("race")]
    public class RaceController : ControllerBase
    {
        private readonly SailDBContext _context;

        public RaceController(SailDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateRace([FromBody] Race race)
        {
            _context.Races.Add(race);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRace), new { id = race.Id }, race);
        }

        [HttpGet]
        public IEnumerable<Race> GetAllRaces()
        {
            return _context.Races.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Race> GetRace(int id)
        {
            var race = _context.Races.Find(id);
            if (race == null) return NotFound();
            return race;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRace(int id)
        {
            var race = _context.Races.Find(id);
            if (race == null) return NotFound();

            _context.Races.Remove(race);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRace(int id, [FromBody] Race updatedRace)
        {
            var race = _context.Races.Find(id);
            if (race == null) return NotFound();

            race.Name = updatedRace.Name;
            race.StartTime = updatedRace.StartTime;
            race.EndTime = updatedRace.EndTime;
            _context.SaveChanges();
            return NoContent();
        }

        //[HttpGet("{id}/participants")]
        //public IEnumerable<Boat> GetParticipants(int id)
        //{
        //    var race = _context.Races.Include(r => r.Participants).FirstOrDefault(r => r.Id == id);
        //    if (race == null) return NotFound();
        //    return race.Participants;
        //}

        //[HttpPut("{id}/participants")]
        //public IActionResult AddParticipant(int id, [FromBody] Boat boat)
        //{
        //    var race = _context.Races.Include(r => r.Participants).FirstOrDefault(r => r.Id == id);
        //    if (race == null) return NotFound();

        //    race.Participants.Add(boat);
        //    _context.SaveChanges();
        //    return NoContent();
        //}

        //[HttpDelete("{id}/participants/{boatId}")]
        //public IActionResult RemoveParticipant(int id, int boatId)
        //{
        //    var race = _context.Races.Include(r => r.Participants).FirstOrDefault(r => r.Id == id);
        //    if (race == null) return NotFound();

        //    var boat = race.Participants.FirstOrDefault(b => b.Id == boatId);
        //    if (boat == null) return NotFound();

        //    race.Participants.Remove(boat);
        //    _context.SaveChanges();
        //    return NoContent();
        //}

        //[HttpGet("{id}/results")]
        //public IActionResult GetResults(int id)
        //{
        //    var race = _context.Races.Include(r => r.Results).FirstOrDefault(r => r.Id == id);
        //    if (race == null) return NotFound();

        //    return Ok(race.Results);
        //}
    }
}
