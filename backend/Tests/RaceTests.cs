using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Controllers;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    public class RaceTests : IDisposable
    {

        private readonly RaceController _controller;
        private readonly RaceService _service;
        private readonly SailDBContext _dbContext;

        public RaceTests()
        {

            _dbContext = CreateDB.InitalizeDB();
            _controller = new RaceController(_dbContext);
            _service = new RaceService(_dbContext);

        }

        public void Dispose()
        {
            CreateDB.DeleteTempDB(_dbContext);
        }

        [Fact]
        public async Task Get_ReturnsListOfRaces()
        {
            var okResult = await _controller.GetRaces();

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Cont_GetRace()
        {
            var ok = await _controller.GetRace(0);

            Assert.IsType<NotFoundResult>(ok);
        }


        [Fact]
        public async Task Service_ReturnsListOfRaces()
        {
            var result = await _service.GetRaces();

            Assert.IsType<List<Race>>(result);
        }

        [Fact]
        public async Task Add_Race()
        {
            Race race = new Race();
            var id = await _service.AddRace(race);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Calc_Res_null()
        {
            var results = _service.CalculateResults(0);

            Assert.NotNull(results);
        }

        [Fact]
        public async Task Calc_Res_invalid()
        {
            var results = _service.CalculateResults(-1);

            Assert.NotNull(results);
        }

        [Fact]
        public async Task Calc_Res_DNE()
        {
            var results = _service.CalculateResults(99999);

            Assert.NotNull(results);
        }

        [Fact]
        public async Task Calculate_Result_Full()
        {
            CreateDB.AddBoats(_dbContext);
            CreateDB.AddRaces(_dbContext);
            _dbContext.SaveChanges();

            List<Track> tracks = new List<Track>();
            List<Boat> boats = _dbContext.Boats.ToList();
            Assert.True(boats.Count >= 10);

            for (int i = 0; i < 10; i++)
            {
                var track = new Track();
                track.Started = DateTime.Now;
                track.Finished = track.Started.AddSeconds(i * 10 + 10);

                boats[i].Rating.CurrentRating = i + 5;
                track.Boat = boats[i];
                track.RaceId = 1;
                track.GpxData = "";

                tracks.Add(track);
            }

            _dbContext.Tracks.AddRange(tracks);
            _dbContext.SaveChanges();

            tracks = _dbContext.Tracks.ToList();
            Assert.True(tracks.Count >= 10);

            var results = await _service.CalculateResults(1, 5);


            Assert.IsType<List<Result>>(results);
            Assert.True(results.Count > 0, $"Result count is {results.Count}");
            Assert.NotEqual(results[0].ElapsedTime, results[0].CorrectedTime);
            Assert.NotEqual(0, results[0].CorrectedTime.TotalMilliseconds);
            Assert.True(results[0].CorrectedTime < results[1].CorrectedTime, $"0 not faster than 1, {results[0].CorrectedTime},{results[1].ElapsedTime} ");
            Assert.True(results[1].CorrectedTime < results[2].CorrectedTime, $"");
            Assert.True(results[0].CorrectedTime < results[9].CorrectedTime);
            Assert.True(results[8].CorrectedTime < results[9].CorrectedTime);

        }

    }
}