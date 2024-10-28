using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    //Tests created with the help of Claude
    public class BoatTests : IDisposable
    {
        private readonly BoatService _service;
        private readonly SailDBContext _dbContext;

        public BoatTests()
        {
            _dbContext = CreateDB.InitalizeDB();
            _service = new BoatService(_dbContext);
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
            await _service.AddBoat(expectedBoat);

            // Act
            var result = await _service.GetBoat(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBoat.Id, result.Id);
            Assert.Equal(expectedBoat.Name, result.Name);
        }

        [Fact]
        public async Task GetBoat_NonExistingId_ReturnsNull()
        {

            // Act
            var result = await _service.GetBoat(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateBoat_ValidBoat_ReturnsCreatedBoat()
        {
            // Arrange
            var newBoat = new Boat
            {
                Name = "Yamaha",
            };

            // Act
            var result = await _service.AddBoat(newBoat);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", result);
        }

        [Fact]
        public async Task UpdateBoat_ExistingBoat_ReturnsUpdatedBoat()
        {

            var updateBoat = new Boat
            {
                Id = 1,
                Name = "SeaRay",
            };
            await _service.AddBoat(updateBoat);

            updateBoat.SailNumber = "1";
            // Act
            var result = await _service.UpdateBoat(updateBoat);

            // Assert
            Assert.True(result);

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
            await _service.AddBoat(existingBoat);

            // Act
            var result = await _service.DeleteBoat(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBoat_NonExistingBoat_ReturnsFalse()
        {
            // Act
            var result = await _service.DeleteBoat(99);

            // Assert
            Assert.False(result);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task CreateBoat_InvalidName(string invalidName)
        {
            // Arrange
            var invalidBoat = new Boat
            {
                Name = invalidName,
            };

            // Act & Assert
            Assert.Null(await _service.AddBoat(invalidBoat));
        }


        [Fact]
        public async Task CreateBoat_MaxLengthExceeded_ThrowsArgumentException()
        {
            // Arrange
            var invalidBoat = new Boat
            {
                Name = new string('A', 999999999) + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() =>
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
