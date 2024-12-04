using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{
    public class RegattaService
    {
        private readonly SailDBContext _dbContext;
        private readonly RaceService _raceService;

        public RegattaService(SailDBContext dbContext)
        {
            _dbContext = dbContext;
            _raceService = new RaceService(dbContext);
        }

        public async Task<int> AddRegatta(Regatta regatta)
        {
            if (regatta != null && regatta.Name != null && regatta.Name.Replace(" ", "").Length > 0)
            {
                var entry = await _dbContext.Regattas.AddAsync(regatta);
                await _dbContext.SaveChangesAsync();
                return entry.Entity.Id;
            }
            return -1;
        }

        //Implement
        //return list of regattas, not full info
        public async Task<List<Regatta>> GetRegattas()
        {
            List<Regatta> regattas = await _dbContext.Regattas
                .Include(r => r.Races)
                .ToListAsync();

            return regattas;
        }

        public async Task<Regatta> GetRegatta(int id)
        {
            return await _dbContext.Regattas
                .Include(r => r.Races)
                .FirstOrDefaultAsync(r => r.Id == id);
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