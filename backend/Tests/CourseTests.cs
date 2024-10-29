using SailMapper.Data;
using SailMapper.Services;

namespace Tests
{
    public class CourseTests : IDisposable
    {
        private readonly CourseService _service;
        private readonly SailDBContext _dbContext;

        public CourseTests()
        {
            _dbContext = CreateDB.InitalizeDB();
            _service = new CourseService(_dbContext);
        }

        public void Dispose()
        {
            CreateDB.DeleteTempDB(_dbContext);
        }
    }
}
