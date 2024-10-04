using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
namespace SailMapper.Data
{

    public class SailDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
           optionsBuilder.UseSqlite("DataSource=SailMapperDb; Cache=Shared");

        public DbSet<Boat> Boats { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMark> CourseMarks { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Regatta> Regattas { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Track> Tracks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // marking entities as keyless
            modelBuilder.Entity<Result>().HasNoKey();
            modelBuilder.Entity<Track>().HasNoKey();

            modelBuilder.Entity<Boat>()
                .HasOne(b => b.Rating)            // A Boat has one Rating
                .WithMany(r => r.Boats)          // A Rating can have many Boats
                .HasForeignKey(b => b.RatingId); // Foreign key in Boat points to RatingId

        }

    }
}