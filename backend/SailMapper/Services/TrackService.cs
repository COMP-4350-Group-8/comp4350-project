using SailMapper.Classes;

namespace SailMapper.Services
{
    public class TrackService
    {
        
        public TrackService() { }

        public Task<string> AddTrack(Track track)
        {
            return Task.FromResult("");
        }

        public Task<Track[]> GetRaceTracks(string id)
        {
            return Task.FromResult(new Track[0]);
        }

        public Task<Track> GetTrack(string id)
        {
            return Task.FromResult(new Track());
        }

        public Task<string> UpdateTrack(Track track)
        {
            return Task.FromResult("");
        }

        public Task<string> GetGPX(string id)
        {
            return Task.FromResult("");
        }
    }
}
