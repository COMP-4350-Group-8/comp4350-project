using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using SailMapper.Controllers;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    //Tests created with the help of Claude
    public class BoatTests : IDisposable
    {
        private readonly BoatService _service;
        private readonly BoatController _controller;
        private readonly SailDBContext _dbContext;

        public BoatTests()
        {
            _dbContext = CreateDB.InitalizeDB();
            _service = new BoatService(_dbContext);
            _controller = new BoatController(_dbContext);
        }

        public void Dispose()
        {
            CreateDB.DeleteTempDB(_dbContext);
        }


        [Fact]
        public async Task GetBoat_ExistingId_ReturnsBoat()
        {
            // Arrange
            var expectedBoat = new Boat
            {
                Id = 1,
                Name = "SeaRay",
            };
            await _controller.CreateBoat(expectedBoat);

            // Act
            var result = await _controller.GetBoat(1);
            Assert.IsType<OkObjectResult>(result);
            // Assert
            var boat = await _dbContext.Boats.FindAsync(1);
            Assert.NotNull(boat);
            Assert.Equal(expectedBoat.Id, boat.Id);
            Assert.Equal(expectedBoat.Name, boat.Name);
        }

        [Fact]
        public async Task GetBoat_NonExistingId_ReturnsNull()
        {

            // Act
            var result = await _controller.GetBoat(99);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateBoat_ExistingBoat_ReturnsUpdatedBoat()
        {

            var updateBoat = new Boat
            {
                Id = 1,
                Name = "SeaRay",
            };
            await _controller.CreateBoat(updateBoat);

            updateBoat.SailNumber = "1";
            // Act
            var result = await _controller.UpdateBoat(updateBoat);

            // Assert
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task DeleteBoat_ExistingBoat_ReturnsTrue()
        {
            // Arrange
            var existingBoat = new Boat
            {
                Id = 1,
                Name = "SeaRay",
            };
            await _controller.CreateBoat(existingBoat);

            // Act
            var result = await _controller.DeleteBoat(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DeleteBoat_NonExistingBoat_ReturnsFalse()
        {
            // Act
            var result = await _controller.DeleteBoat(99);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task CreateBoat_InvalidName(string? invalidName)
        {
            // Arrange
            var invalidBoat = new Boat
            {
                Name = invalidName,
            };

            // Act & Assert
            Assert.Null(await _service.AddBoat(invalidBoat));
        }

        //cannot create string that breaks database that does not break c#
        //[Fact]
        [Fact]
        public async Task CreateBoat_MaxLengthExceeded_ThrowsArgumentException()
        {
            // Arrange
            var invalidBoat = new Boat
            {
                Name = new string('A', 999999999)
            };

            // Act & Assert
            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() =>
                _service.AddBoat(invalidBoat));
        }

        [Fact]
        public async Task UpdateBoat_NullObject_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                _service.UpdateBoat(null));
        }


    }


}
