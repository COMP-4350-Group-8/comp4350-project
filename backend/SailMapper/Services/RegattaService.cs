using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{
    public class RegattaService
    {
        private readonly SailDBContext _dbContext;
        private readonly RaceService _raceService;

        public RegattaService()
        {
            _dbContext = new SailDBContext();
            _raceService = new RaceService();
        }

        public async Task<int> AddRegatta(Regatta regatta)
        {
            await _dbContext.Regattas.AddAsync(regatta);
            await _dbContext.SaveChangesAsync();
            return regatta.Id;
        }

        //Implement
        //return list of regattas, not full info
        public async Task<List<Regatta>> GetRegattas()
        {
            List<Regatta> regattas = _dbContext.Regattas.ToList();

            return regattas;
        }

        public Task<Regatta> GetRegatta(int id)
        {
            return GetRegattaEntity(id);
        }

        public async Task<bool> DeleteRegatta(int id)
        {
            Regatta regatta = await GetRegattaEntity(id);
            if (regatta != null)
            {
                _dbContext.Regattas.Remove(regatta);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRegatta(Regatta regattaUpdate)
        {
            var regatta = _dbContext.Regattas.Update(regattaUpdate);
            if (regatta != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddRace(int id, int regattaid)
        {
            Regatta regatta = await GetRegattaEntity(regattaid);
            Race race = await GetRaceEntity(id);

            if (race != null && regatta != null)
            {
                if (regatta.Races == null)
                {
                    regatta.Races = new List<Race>();
                }
                regatta.Races.Add(race);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveRace(int id, int regattaid)
        {
            Regatta regatta = await GetRegattaEntity(regattaid);
            Race race = await GetRaceEntity(id);

            if (race != null && regatta != null && regatta.Races != null)
            {
                regatta.Races.Remove(race);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<List<Result>>> GetRegattaResults(int id)
        {
            Regatta regatta = await GetRegattaEntity(id);

            List<List<Result>> results = new List<List<Result>>();

            foreach (Race race in regatta.Races)
            {
                results.Add(await _raceService.GetResults(race));
            }

            return results;
        }


        private async Task<Regatta> GetRegattaEntity(int id)
        {
            return await _dbContext.Regattas.FindAsync(id);
        }
        private async Task<Race> GetRaceEntity(int id)
        {
            return await _dbContext.Races.FindAsync(id);
        }

    }
}