using SailMapper.Classes;

namespace SailMapper.Services
{
    public class RegattaService
    {
        //Implement
        //return id
        public Task<string> AddRegatta(Regatta regatta)
        {
            return Task.FromResult("");
        }

        //Implement
        //return list of regattas, not full info
        public Task<Regatta[]> GetRegattas()
        {
            return Task.FromResult(new Regatta[0]);
        }

        public Task<Regatta> GetRegatta()
        {
            return Task.FromResult(new Regatta());
        }

        public Task<Regatta> GetRegatta(string id)
        {
            return Task.FromResult(new Regatta());
        }

        public Task<bool> DeleteRegatta(string id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> UpdateRegatta(string id, Regatta regatta)
        {
            return Task.FromResult(false);
        }

        public Task<bool> AddRace(string id, string raceid)
        {
            return Task.FromResult(false);
        }
        public Task<bool> RemoveRace(string id, string raceid)
        {
            return Task.FromResult(false);
        }
        public Task<Result[][]> GetRegattaResults(string id )
        {
            return Task.FromResult(new Result[0][]);
        }





    }
}
