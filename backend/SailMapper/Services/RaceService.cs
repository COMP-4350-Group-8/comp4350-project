using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{

    public class RaceService
    {

        private readonly SailDBContext _dbContext;
        public RaceService() { }

        //Implement
        //return id
        public async Task<string> AddRace(Race race)
        {
            var id = await _dbContext.Races.AddAsync(race);
            return id.ToString();
        }

        //Implement
        //return list of races, not full info
        public Task<Race[]> GetRaces()
        {
            return Task.FromResult(new Race[0]);
        }

        public Task<Race> GetRace()
        {
            return Task.FromResult(new Race());
        }

        public Task<Race> GetRace(string id)
        {
            return Task.FromResult(new Race());
        }

        public Task<bool> DeleteRace(string id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> UpdateRace(string id, Race race)
        {
            return Task.FromResult(false);
        }

        public Task<Boat[]> GetParticipants(string id)
        {
            return Task.FromResult(new Boat[0]);
        }

        public Task<bool> AddParticipant(string id, string boat)
        {
            return Task.FromResult(false);
        }

        public Task<bool> RemoveParticipant(string id, string boat)
        {
            return Task.FromResult(false);
        }

        public Task<Result[]> GetResults(string id)
        {
            return Task.FromResult<Result[]>(new Result[0]);
        }






    }
}
