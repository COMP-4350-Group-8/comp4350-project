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

            double slope = (one.Latitude - two.Latitude) / (one.Longitude - two.Longitude);
            double intercept = two.Latitude - slope * one.Longitude;

            bool? side = null;
            XmlNode prev = null;

            foreach (XmlNode wpt in track.DocumentElement.ChildNodes)
            {
                double lat = Convert.ToDouble(wpt.Attributes["lat"].Value);
                double lon = Convert.ToDouble(wpt.Attributes["lon"].Value);

                if (lat != null && lon != null)
                {
                    double expected = lon * slope + intercept;

                    if (side == null)
                    {
                        side = expected < lat;
                    }
                    else if (expected < lat != side)
                    {
                        // check that the side switching occurs within the two ends

                        if (lat < one.Latitude && lat > two.Latitude || lat > one.Latitude && lat < two.Latitude)
                        {
                            if (lon < one.Longitude && lon > two.Longitude || lon > one.Longitude && lon < two.Longitude)
                            {

                                DateTime currTime = DateTime.Parse(wpt.Attributes["time"].Value);
                                DateTime prevTime = DateTime.Parse(prev.Attributes["time"].Value);


                                long wptSec = ((DateTimeOffset)currTime).ToUnixTimeMilliseconds();
                                long prevSec = ((DateTimeOffset)prevTime).ToUnixTimeMilliseconds();

                                finish = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((wptSec - prevSec) / 2 + prevSec);

                                break;
                            }
                        }
                    }
                }

                prev = wpt;
            }

            return finish;
        }

        private async Task<Track> GetTrackEntity(int id)
        {
            return await _dbContext.Tracks.FindAsync(id);
        }
    }
}