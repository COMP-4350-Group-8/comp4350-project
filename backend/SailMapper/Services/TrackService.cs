using SailMapper.Classes;
using SailMapper.Data;
using System.Xml;

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

        public async Task<bool> CalcFinish(Track track)
        {
            if (track == null)
            {
                return false;
            }

            //find finish line points

            if (track.Race == null)
            {
                track.Race = await _dbContext.Races.FindAsync(track.RaceId);
            }

            if (track.Race.Course == null)
            {
                track.Race.Course = await _dbContext.Courses.FindAsync(track.Race.CourseId);
            }

            List<CourseMark> line = _dbContext.CourseMarks.Where(m => m.CourseId == track.Race.CourseId && m.IsStartLine == false).ToList();

            DateTime finish = CalcFinish(track.GpxData, line);

            return true;
        }


        private DateTime CalcFinish(string gpx, List<CourseMark> marks)
        {
            DateTime finish = new DateTime();

            if (gpx == null || marks != null || marks.Count < 2 || marks.Count > 2)
            {
                throw new ArgumentException("invalid arguments");
            }

            // parse gpx
            XmlDocument track = new XmlDocument();
            track.LoadXml(gpx);

            // find points before and after finish line
            DateTime closestPPoint = new DateTime();
            float closestP = 0;
            DateTime closestNPoint = new DateTime();
            float closestN = 0;

            var earthRadius = 6371;

            foreach (XmlNode wpt in track.DocumentElement.ChildNodes)
            {
                var lat = wpt.Attributes["lat"];
                var lon = wpt.Attributes["lon"];

                if (lat != null && lon != null)
                {
                    // calculate distance to finish line

                }

            }

            // take weighted average of those times
            var pSeconds = ((DateTimeOffset)closestPPoint).ToUnixTimeMilliseconds();
            var nSeconds = ((DateTimeOffset)closestNPoint).ToUnixTimeMilliseconds();

            var pWeighted = pSeconds * (closestP / (closestP + closestN));
            var nWeighted = nSeconds * (closestN / (closestP + closestN));

            var finishSeconds = pWeighted + nWeighted;


            return finish;
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