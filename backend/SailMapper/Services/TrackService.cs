using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{
    public class TrackService
    {
        private readonly SailDBContext _dbContext;


        public TrackService(SailDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddTrack(Track track)
        {
            await _dbContext.Tracks.AddAsync(track);
            await _dbContext.SaveChangesAsync();
            return track.Id;
        }

        public async Task<List<Track>> GetRaceTracks()
        {
            List<Track> tracks = _dbContext.Tracks.ToList();
            return tracks;
        }

        public async Task<Track> GetTrack(int id)
        {
            return await GetTrackEntity(id);
        }

        public async Task<bool> UpdateTrack(Track trackUpdate)
        {
            var track = _dbContext.Tracks.Update(trackUpdate);
            if (track != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Track>> GetRaceTracks(int id)
        {
            List<Track> tracks = _dbContext.Tracks.Where(t => t.RaceId == id).ToList();

            return tracks;
        }

        public async Task<string> GetGPX(int id)
        {
            return _dbContext.Tracks.FindAsync(id).Result.GpxData;
        }

        private async Task<Track> GetTrackEntity(int id)
        {
            return await _dbContext.Tracks.FindAsync(id);
        }
    }
}