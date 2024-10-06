
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Controllers;
using SailMapper.Services;

namespace Tests
{
    public class RaceTests
    {

        private readonly RaceController _controller;
        private readonly RaceService _service;

        public RaceTests()
        {
            _controller = new RaceController();
            _service = new RaceService();
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
           // var id = await _service.AddRace(race);
            
            //Assert.NotNull(id);
        }
    }
}