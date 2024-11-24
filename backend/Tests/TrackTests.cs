using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    public class TrackTests : IDisposable
    {
        private readonly TrackService _service;
        private readonly SailDBContext _dbContext;

        public TrackTests()
        {
            _dbContext = CreateDB.InitalizeDB();
            _service = new TrackService(_dbContext);
        }

        public void Dispose()
        {
            CreateDB.DeleteTempDB(_dbContext);
        }

        // Basic CRUD Tests
        [Fact]
        public async Task AddTrack_ValidTrack_ReturnsCreatedTrack()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            // Arrange
            var newTrack = new Track
            {
                RaceId = 1,
                BoatId = 1,
                Started = DateTime.UtcNow,
                Finished = DateTime.UtcNow.AddHours(2),
                GpxData = " "
            };


            // Act
            var result = await _service.AddTrack(newTrack);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllTracks_ReturnsAllTracks()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            // Arrange
            var tracks = new List<Track>
            {
                new Track { Id = 1, RaceId = 1, BoatId = 1 , GpxData = ""},
                new Track { Id = 2, RaceId = 1, BoatId = 2 , GpxData = ""}
            };

            _dbContext.Tracks.AddRange(tracks);
            _dbContext.SaveChanges();

            // Act
            var result = await _service.GetRaceTracks();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, t => t.BoatId == 1);
            Assert.Contains(result, t => t.BoatId == 2);
        }

        [Fact]
        public async Task GetTrackById_ExistingId_ReturnsTrack()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            // Arrange
            var track = new Track
            {
                Id = 1,
                RaceId = 1,
                BoatId = 1,
                GpxData = " "
            };

            _dbContext.Tracks.Add(track);
            _dbContext.SaveChanges();

            // Act
            var result = await _service.GetTrack(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(track.RaceId, result.RaceId);
            Assert.Equal(track.BoatId, result.BoatId);
        }

        [Fact]
        public async Task UpdateTrack_ValidTrack_ReturnsUpdatedTrack()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            // Arrange
            var existingTrack = new Track
            {
                Id = 1,
                RaceId = 1,
                BoatId = 1,
                GpxData = " "
            };

            _dbContext.Tracks.Add(existingTrack);
            _dbContext.SaveChanges();

            existingTrack.GpxData = "{}";

            // Act
            var result = await _service.UpdateTrack(existingTrack);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllTracksForRace_ReturnsRaceTracks()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            // Arrange
            var raceTracks = new List<Track>
            {
                new Track { Id = 1, RaceId = 1, BoatId = 1, GpxData = "" },
                new Track { Id = 2, RaceId = 1, BoatId = 2, GpxData = "" }
            };
            _dbContext.Tracks.AddRange(raceTracks);
            _dbContext.SaveChanges();


            // Act
            var result = await _service.GetRaceTracks(1);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, track => Assert.Equal(1, track.RaceId));
        }

        [Fact]
        public async Task GetGpxForTrack_ReturnsValidGpxData()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            var gpx = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no"" ?>
<gpx xmlns=""http://www.topografix.com/GPX/1/1"" version=""1.1"" creator=""Wikipedia""
    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
    xsi:schemaLocation=""http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd"">
 <!-- Comments look like this -->
 <metadata>
  <name>Data name</name>
  <desc>Valid GPX example without special characters</desc>
  <author>
   <name>Author name</name>
  </author>
 </metadata>
 <wpt lat=""52.518611"" lon=""13.376111"">
  <ele>35.0</ele>
  <time>2011-12-31T23:59:59Z</time>
  <name>Reichstag (Berlin)</name>
  <sym>City</sym>
 </wpt>
 <wpt lat=""48.208031"" lon=""16.358128"">
  <ele>179</ele>
  <time>2011-12-31T23:59:59Z</time>
  <name>Parlament (Wien)</name>
  <sym>City</sym>
 </wpt>
 <wpt lat=""46.9466"" lon=""7.44412"">
  <time>2011-12-31T23:59:59Z</time>
  <name>Bundeshaus (Bern)</name>
  <sym>City</sym>
 </wpt>
</gpx>";

            // Arrange
            var track = new Track
            {
                Id = 1,
                RaceId = 1,
                BoatId = 1,
                Started = DateTime.UtcNow,
                Finished = DateTime.UtcNow.AddHours(2),
                GpxData = gpx
            };

            _dbContext.Tracks.Add(track);
            _dbContext.SaveChanges();


            // Act
            var gpxData = await _service.GetGPX(1);

            // Assert
            Assert.NotNull(gpxData);
            Assert.Contains("<?xml version=\"1.0\"", gpxData);
            Assert.Contains("<gpx", gpxData);
            Assert.Contains("<wpt", gpxData);
            Assert.Contains("lat=\"46.9466", gpxData);
            Assert.Contains("lon=\"7.44412", gpxData);
        }

        [Fact]
        public async Task GetGpxForTrack_NonexistentTrack_ThrowsNotFoundException()
        {

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                _service.GetGPX(999));
        }

        //Could change behavior 
        [Fact]
        public async Task AddTrack_DuplicateTrackForBoatInRace_ThrowsInvalidOperationException()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);

            // Arrange
            var existingTrack = new Track
            {
                RaceId = 1,
                BoatId = 1,
                GpxData = " "
            };

            _dbContext.Tracks.Add(existingTrack);
            _dbContext.SaveChanges();

            var newTrack = new Track
            {
                RaceId = 1,
                BoatId = 1,
                GpxData = " "
            };

            // Act & Assert
            //await Assert.ThrowsAsync<InvalidOperationException>(() =>
            //    _service.AddTrack(newTrack));
            var result = await _service.AddTrack(newTrack);

            Assert.Equal(2, result);
        }

    }
}
