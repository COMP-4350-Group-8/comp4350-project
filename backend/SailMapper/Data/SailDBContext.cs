using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
namespace SailMapper.Data
{

    public class SailDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
           optionsBuilder.UseMySql("DataSource=SailMapperDb; Cache=Shared");

        public DbSet<Boat> Boats { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMark> CourseMarks { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Regatta> Regattas { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Track> Tracks { get; set; }


    }
}