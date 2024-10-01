using SailMapper.Classes;

namespace SailMapper.Services
{
    public class BoatService
    {
        //Implement
        //return id
        public Task<string> AddBoat(Boat boat)
        {
            return Task.FromResult("");
        }

        //Implement
        //return list of boats, not full info
        public Task<Boat[]> GetBoats()
        {
            return Task.FromResult(new Boat[0]);
        }

        public Task<Boat> GetBoat()
        {
            return Task.FromResult(new Boat());
        }

        public Task<Boat> GetBoat(string id)
        {
            return Task.FromResult(new Boat());
        }

        public Task<bool> DeleteBoat(string id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> UpdateBoat(Boat boat)
        {
            return Task.FromResult(false);
        }


    }
}
