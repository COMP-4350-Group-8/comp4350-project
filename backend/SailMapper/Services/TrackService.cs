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

            //TODO check that there are only two points
            DateTime finish = CalcFinish(track.GpxData, line[0], line[0]);

            return true;
        }


        public DateTime CalcFinish(string gpx, CourseMark one, CourseMark two)
        {
            DateTime finish = new DateTime();

            if (gpx == null || one == null || two == null)
            {
                throw new ArgumentException("invalid arguments");
            }

            // parse gpx
            XmlDocument track = new XmlDocument();
            track.LoadXml(gpx);

            // find points before and after finish line
            DateTime closestPPoint = new DateTime();
            double closestP = 0;
            DateTime closestNPoint = new DateTime();
            double closestN = 0;

            var earthRadius = 6371;

            foreach (XmlNode wpt in track.DocumentElement.ChildNodes)
            {
                double lat = Convert.ToDouble(wpt.Attributes["lat"].Value);
                double lon = Convert.ToDouble(wpt.Attributes["lon"].Value);

                if (lat != null && lon != null)
                {
                    // calculate distance to finish line

                    //find bearing from point to ends of line

                    double bering1 = Bearing(one.Latitude, one.Longitude, lat, lon);
                    double bering2 = Bearing(two.Latitude, two.Longitude, lat, lon);


                    //find distance from point to one end (spherical cosines)
                    //distanceAC = acos( sin(φ₁)*sin(φ₂) + cos(φ₁)*cos(φ₂)*cos(Δλ) )*R
                    double distace = Math.Acos(Math.Sin(one.Latitude) * Math.Sin(lat) + Math.Cos(one.Latitude) * Math.Cos(lat) * Math.Cos(lon - one.Longitude)) * earthRadius;


                    //find cross track diffrence
                    //distance = asin(sin(distanceAC/ R) * sin(bearing1 − bearing2)) * R
                    double min_distance = Math.Asin(Math.Sin(distace / earthRadius) * Math.Sin(bering1 - bering2)) * earthRadius;

                    if (min_distance > 0 && (closestP == 0 || min_distance < closestP))
                    {
                        closestP = min_distance;
                        closestPPoint = DateTime.Parse(wpt.Attributes["time"].Value);
                    }
                    else if (min_distance < 0 && (closestN == 0 || min_distance < closestN))
                    {
                        closestN = min_distance;
                        closestNPoint = DateTime.Parse(wpt.Attributes["time"].Value);
                    }
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

        private double Bearing(double lat1, double lon1, double lat2, double lon2)
        {

            //bearingAC = atan2( sin(Δλ)*cos(φ₂), cos(φ₁)*sin(φ₂) − sin(φ₁)*cos(φ₂)*cos(Δλ) )  
            double y = Math.Sin(lon2 - lon1) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lat2 - lat1);

            double bearing = Math.Atan2(y, x);
            bearing = 360 - ((bearing + 360) % 360);

            return bearing;



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