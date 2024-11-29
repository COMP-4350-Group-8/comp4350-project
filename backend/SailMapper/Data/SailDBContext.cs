using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SailMapper.Data
{

    public class SailDBContext : DbContext
    {


        public SailDBContext(DbContextOptions<SailDBContext> options) : base(options)
        {
        }

        public DbSet<Boat> Boats { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMark> CourseMarks { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Regatta> Regattas { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("MySqlConn"), new MySqlServerVersion(new Version(8, 0, 2)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseMark>()
                .HasOne(m => m.Gate)
                .WithMany()
                .HasForeignKey(m => m.GateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}