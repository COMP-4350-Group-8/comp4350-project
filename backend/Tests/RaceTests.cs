
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public RaceTests(SailDBContext dbContext)
        {
            _dbContext = dbContext;
            _controller = new RaceController(dbContext);
            _service = new RaceService(dbContext);
        }

        [Fact]
        public async void Get_ReturnsListOfRaces()
        {
            var okResult = await _controller.GetRaces();

            Assert.IsType<Ok<List<Race>>>(okResult);
        }

        [Fact]
        public async void Service_ReturnsListOfRaces()
        {
            var result = await _service.GetRaces();

            Assert.IsType<List<Race>>(result);
        }

        [Fact]
        public async void Add_Race()
        {
            Race race = new Race();
            var id = await _service.AddRace(race);

            Assert.NotNull(id);
        }
    }
}