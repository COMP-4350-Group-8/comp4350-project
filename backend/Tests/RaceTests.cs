using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using SailMapper.Controllers;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    public class RaceTests
    {

        private readonly RaceController _controller;
        private readonly RaceService _service;
        private readonly SailDBContext _dbContext;

        public RaceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SailDBContext>();

            //var connectionString = "Server = localhost;Database = SailDB;User = root; Password = Lowisa;";
            //Connect to dev DB
            optionsBuilder.UseMySql("Server=localhost;Database=SailDB;User=root;Password=Lowisa;", new MySqlServerVersion(new Version(8, 0, 2)));



            _dbContext = new SailDBContext(optionsBuilder.Options);
            _controller = new RaceController(_dbContext);
            _service = new RaceService(_dbContext);
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

            Assert.Equal(-1, id);
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

        // [Fact]
        [Fact]
        public async Task Calculate_Result_Full()
        {
            List<Track> tracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                var track = new Track();
                track.Started = DateTime.Now;
                track.Finished = track.Started;
                track.Finished.AddSeconds(i * 10 + 10);
                track.Boat = new Boat();
                track.Boat.Rating = new Rating();
                track.Boat.Rating.CurrentRating = i * 5 + 5;
                track.RaceId = 0;

                tracks.Add(track);
            }

            _dbContext.Tracks.AddRange(tracks);

            var results = await _service.CalculateResults(0, 0);


            Assert.IsType<List<Result>>(results);
            Assert.True(results.Count > 0, $"Result count is {results.Count}");
            Assert.Equal(10, results.Count); //TODO: FIX
            Assert.True(results[0].CorrectedTime < results[1].CorrectedTime);
            Assert.True(results[1].CorrectedTime < results[2].CorrectedTime);
            Assert.True(results[0].CorrectedTime < results[9].CorrectedTime);
            Assert.True(results[8].CorrectedTime < results[9].CorrectedTime);

        }

    }
}