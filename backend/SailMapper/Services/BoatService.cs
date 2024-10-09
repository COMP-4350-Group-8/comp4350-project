using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{
    public class BoatService
    {
        private readonly SailDBContext _dbContext;

        public BoatService(SailDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddBoat(Boat boat)
        {
            var id = await _dbContext.Boats.AddAsync(boat);
            await _dbContext.SaveChangesAsync();
            return id.ToString();
        }

        public async Task<List<Boat>> GetBoats()
        {
            List<Boat> boats = _dbContext.Boats.ToList();
            return boats;
        }

        public async Task<Boat> GetBoat(int id)
        {
            Boat boat = await GetBoatEntity(id);

            return boat;
        }

        public async Task<bool> DeleteBoat(int id)
        {
            Boat boat = await GetBoatEntity(id);
            if (boat != null)
            {
                _dbContext.Boats.Remove(boat);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateBoat(Boat boatUpdate)
        {
            var boat = _dbContext.Boats.Update(boatUpdate);
            if (boat != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<Boat> GetBoatEntity(int id)
        {
            return await _dbContext.Boats.FindAsync(id);
        }


    }
}