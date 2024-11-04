using SailMapper.Classes;
using SailMapper.Controllers;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    public class RegattaTests : IDisposable
    {
        private readonly RegattaController _controller;
        private readonly RegattaService _service;
        private readonly SailDBContext _dbContext;

        public RegattaTests()
        {
            _dbContext = CreateDB.InitalizeDB();
            _controller = new RegattaController(_dbContext);
            _service = new RegattaService(_dbContext);
        }

        public void Dispose()
        {
            CreateDB.DeleteTempDB(_dbContext);
        }

        // Basic CRUD Tests
        [Fact]
        public async Task CreateRegatta_ValidRegatta_ReturnsCreatedRegatta()
        {
            // Arrange
            var newRegatta = new Regatta
            {
                Name = "Summer Sailing Series",
                Description = "Description",
            };


            // Act
            int result = await _service.AddRegatta(newRegatta);

            // Assert
            Assert.NotEqual(-1, result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllRegattas_ReturnsAllRegattas()
        {
            // Arrange
            var regattas = new List<Regatta>
        {
            new Regatta { Id = 1, Name = "Summer Series", Description = "Description"},
            new Regatta { Id = 2, Name = "Winter Series", Description = "Description"}
        };

            _dbContext.Regattas.AddRange(regattas);
            _dbContext.SaveChanges();

            // Act
            var result = await _service.GetRegattas();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Name == "Summer Series");
            Assert.Contains(result, r => r.Name == "Winter Series");
        }

        [Fact]
        public async Task GetRegatta_ExistingId_ReturnsRegatta()
        {
            // Arrange
            var regatta = new Regatta
            {
                Id = 1,
                Name = "Summer Series",
                Races = new List<Race>(),
                Description = "Description",
            };

            _dbContext.Regattas.Add(regatta);
            _dbContext.SaveChanges();

            // Act
            var result = await _service.GetRegatta(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(regatta.Name, result.Name);
        }

        [Fact]
        public async Task UpdateRegatta_ValidRegatta_ReturnsUpdatedRegatta()
        {
            // Arrange
            var updateRegatta = new Regatta
            {
                Id = 1,
                Name = "Summer Series",
                Description = "Description",
            };

            _dbContext.Regattas.Add(updateRegatta);
            _dbContext.SaveChanges();

            updateRegatta.Name = "Updated Summer Series";

            // Act
            var result = await _service.UpdateRegatta(updateRegatta);

            // Assert
            Assert.True(result);
            Assert.Equal(_dbContext.Regattas.Find(1).Name, updateRegatta.Name);
        }

        // Race Management Tests
        [Fact]
        public async Task AddRace_ValidRace_ReturnsUpdatedRegatta()
        {
            // Arrange
            var regatta = new Regatta
            {
                Id = 1,
                Name = "Summer Series",
                Races = new List<Race>(),
                Description = "Description",
            };

            var newRace = new Race
            {
                StartTime = DateTime.Now.AddDays(1),
                Name = "race"
            };

            _dbContext.Regattas.Add(regatta);
            _dbContext.Races.Add(newRace);
            _dbContext.SaveChanges();

            // Act
            var result = await _service.AddRace(1, 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteRace_ExistingRace_ReturnsUpdatedRegatta()
        {
            // Arrange
            var regatta = new Regatta
            {
                Id = 1,
                Name = "",
                Description = "Description",
                Races = new List<Race>
            {
                new Race { Id = 1, Name = "" }
            }
            };

            _dbContext.Regattas.Add(regatta);

            // Act
            var result = await _service.RemoveRace(1, 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetResults_ReturnsRaceResults()
        {
            // Arrange
            CreateDB.AddBoats(_dbContext);
            List<Boat> boats = _dbContext.Boats.ToList();

            var results = new List<Result>
                {
                    new Result { BoatId = 1, Points = 1, CorrectedTime = TimeSpan.FromHours(2) },
                    new Result { BoatId = 2, Points = 2, CorrectedTime= TimeSpan.FromHours(2.5) }
                };


            var regatta = new Regatta
            {
                Id = 1,
                Name = "",
                Description = "Description",
                Races = new List<Race>
            {
                new Race
                {
                    Id = 1,
                    Name = "",
                    Results = results,
                    Participants = new List<Boat>
                    {
                        boats[0],
                        boats[1],
                    }
                }
            }
            };

            _dbContext.Regattas.Add(regatta);
            _dbContext.Results.AddRange(results);
            _dbContext.SaveChanges();

            // Act
            var result = await _service.GetRegattaResults(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(2, result[0].Count());
            Assert.Equal(1, result[0][0].Points);
            Assert.Equal(2, result[0][1].Points);
        }

        // Edge Cases and Validation Tests
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task CreateRegatta_InvalidName_ThrowsArgumentException(string? invalidName)
        {
            // Arrange
            var invalidRegatta = new Regatta
            {
                Name = invalidName,
                Description = "",
            };


            Assert.Equal(-1, await _service.AddRegatta(invalidRegatta));
        }
    }
}
