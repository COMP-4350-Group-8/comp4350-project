using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SailMapper.Classes;
using SailMapper.Data;

namespace Tests
{
    internal static class CreateDB
    {

        public static SailDBContext InitalizeDB()
        {
            string testDBName = "temp_test_sail_mapper";
            string connectionString = $"Server=localhost;Database={testDBName};User=root;Password=potato;";
            using (var connection = new MySqlConnection("Server=localhost;User=root;Password=potato"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"DROP DATABASE IF EXISTS `{testDBName}`";
                    command.CommandText = $"CREATE DATABASE `{testDBName}`";
                    command.ExecuteNonQuery();
                }
            }

            var optionsBuilder = new DbContextOptionsBuilder<SailDBContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            var context = new SailDBContext(optionsBuilder.Options);
            context.Database.Migrate();
            return context;

        }

        public static void DeleteTempDB(SailDBContext context)
        {
            string dbName = context.Database.GetDbConnection().Database;
            context.Dispose();

            using (var connection = new MySqlConnection("Server=localhost;User=root;Password=potato"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"DROP DATABASE IF EXISTS `{dbName}`";
                    command.ExecuteNonQuery();
                }
            }
        }

        //test data generated with claude.ai

        public static void AddCourses(SailDBContext context)
        {
            // Define Courses
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Harbor Loop",
                    Description = "A scenic route around the harbor",
                    courseMarks = new List<CourseMark>()
                },
                new Course
                {
                    Id = 2,
                    Name = "Offshore Challenge",
                    Description = "A demanding offshore course",
                    courseMarks = new List<CourseMark>()
                },
                new Course
                {
                    Id = 3,
                    Name = "Island Circuit",
                    Description = "A route circling the nearby islands",
                    courseMarks = new List<CourseMark>()
                }
            };

            // Define Course Marks
            var courseMarks = new List<CourseMark>
            {
                // Marks for Harbor Loop
                new CourseMark
                {
                    Id = 1,
                    Latitude = 41.3950f,
                    Longitude = -71.9650f,
                    Description = "Harbor Entrance Buoy",
                    Rounding = true,
                    IsStartLine = true,
                    CourseId = 1
                },
                new CourseMark
                {
                    Id = 2,
                    Latitude = 41.3900f,
                    Longitude = -71.9600f,
                    Description = "Lighthouse Point",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 1
                },
                new CourseMark
                {
                    Id = 3,
                    Latitude = 41.3975f,
                    Longitude = -71.9700f,
                    Description = "Harbor Bridge",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 1
                },
                new CourseMark
                {
                    Id = 4,
                    Latitude = 41.3950f,
                    Longitude = -71.9650f,
                    Description = "Finish Line",
                    Rounding = false,
                    IsStartLine = false,
                    CourseId = 1
                },

                // Marks for Offshore Challenge
                new CourseMark
                {
                    Id = 5,
                    Latitude = 41.4000f,
                    Longitude = -72.0000f,
                    Description = "Start/Finish Line",
                    Rounding = false,
                    IsStartLine = true,
                    CourseId = 2
                },
                new CourseMark
                {
                    Id = 6,
                    Latitude = 41.4500f,
                    Longitude = -72.1000f,
                    Description = "Offshore Buoy 1",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 2
                },
                new CourseMark
                {
                    Id = 7,
                    Latitude = 41.5000f,
                    Longitude = -72.0500f,
                    Description = "Offshore Buoy 2",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 2
                },

                // Marks for Island Circuit
                new CourseMark
                {
                    Id = 8,
                    Latitude = 41.3800f,
                    Longitude = -71.9500f,
                    Description = "Start Line",
                    Rounding = false,
                    IsStartLine = true,
                    CourseId = 3
                },
                new CourseMark
                {
                    Id = 9,
                    Latitude = 41.3700f,
                    Longitude = -71.9400f,
                    Description = "South Island Marker",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 3
                },
                new CourseMark
                {
                    Id = 10,
                    Latitude = 41.3900f,
                    Longitude = -71.9300f,
                    Description = "East Island Marker",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 3
                },
                new CourseMark
                {
                    Id = 11,
                    Latitude = 41.4000f,
                    Longitude = -71.9600f,
                    Description = "North Island Marker",
                    Rounding = true,
                    IsStartLine = false,
                    CourseId = 3
                },
                new CourseMark
                {
                    Id = 12,
                    Latitude = 41.3800f,
                    Longitude = -71.9500f,
                    Description = "Finish Line",
                    Rounding = false,
                    IsStartLine = false,
                    CourseId = 3
                }
            };

            // Associate CourseMarks with their respective Courses
            foreach (var mark in courseMarks)
            {
                courses.Find(c => c.Id == mark.CourseId)?.courseMarks?.Add(mark);
            }

            // Example of creating a gate
            courseMarks[5].Gate = courseMarks[6]; // Make Offshore Buoy 1 and 2 a gate


            context.CourseMarks.AddRange(courseMarks);
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }

        public static void AddBoats(SailDBContext context)
        {
            var boats = new List<Boat>
            {
                new Boat { Name = "Sea Spirit", Class = "Cruiser", SailNumber = "ABC123", Skipper = "John Doe", RatingId = 1 },
                new Boat { Name = "Wind Chaser", Class = "Racer", SailNumber = "XYZ789", Skipper = "Jane Smith", RatingId = 2 },
                new Boat { Name = "Salty Dog", Class = "Fishing", SailNumber = "FIS456", Skipper = "Mike Johnson", RatingId = 3 },
                new Boat { Name = "Ocean Breeze", Class = "Yacht", SailNumber = "YAC789", Skipper = "Emily Brown", RatingId = 4 },
                new Boat { Name = "Wave Rider", Class = "Catamaran", SailNumber = "CAT234", Skipper = "David Lee", RatingId = 5 },
                new Boat { Name = "Serenity Now", Class = "Sailboat", SailNumber = "SAI567", Skipper = "Sarah Wilson", RatingId = 6 },
                new Boat { Name = "Knot Working", Class = "Motorboat", SailNumber = "MOT890", Skipper = "Chris Taylor", RatingId = 7 },
                new Boat { Name = "Aquaholic", Class = "Speedboat", SailNumber = "SPE123", Skipper = "Lisa Anderson", RatingId = 8 },
                new Boat { Name = "Nauti Buoy", Class = "Dinghy", SailNumber = "DIN456", Skipper = "Tom Harris", RatingId = 9 },
                new Boat { Name = "Seas the Day", Class = "Houseboat", SailNumber = "HOU789", Skipper = "Emma Martinez", RatingId = 10 },
                new Boat { Name = "Unsinkable II", Class = "Trawler", SailNumber = "TRA234", Skipper = "Robert Clark", RatingId = 11 },
                new Boat { Name = "Pier Pressure", Class = "Pontoon", SailNumber = "PON567", Skipper = "Olivia White", RatingId = 12 },
                new Boat { Name = "Knot on Call", Class = "Kayak", SailNumber = "KAY890", Skipper = "Daniel Green", RatingId = 13 },
                new Boat { Name = "Moor the Merrier", Class = "Trimaran", SailNumber = "TRI123", Skipper = "Sophia Lee", RatingId = 14 },
                new Boat { Name = "Sail La Vie", Class = "Schooner", SailNumber = "SCH456", Skipper = "James Wilson", RatingId = 15 },
                new Boat { Name = "Dock Holiday", Class = "Jet Ski", SailNumber = "JET789", Skipper = "Ava Johnson", RatingId = 16 },
                new Boat { Name = "Idle Hour", Class = "Canoe", SailNumber = "CAN234", Skipper = "William Brown", RatingId = 17 },
                new Boat { Name = "Knotty Buoy", Class = "Inflatable", SailNumber = "INF567", Skipper = "Mia Taylor", RatingId = 18 },
                new Boat { Name = "Vitamin Sea", Class = "Bowrider", SailNumber = "BOW890", Skipper = "Ethan Davis", RatingId = 19 },
                new Boat { Name = "Seas the Moment", Class = "Deck Boat", SailNumber = "DEC123", Skipper = "Isabella Moore", RatingId = 20 }
            };

            var ratings = new List<Rating>
            {
                new Rating { Id = 1, BaseRating = 100, SpinnakerAdjustment = -3, Adjustment = 2, CurrentRating = 99 },
                new Rating { Id = 2, BaseRating = 95, SpinnakerAdjustment = -2, Adjustment = 1, CurrentRating = 94 },
                new Rating { Id = 3, BaseRating = 105, SpinnakerAdjustment = 0, Adjustment = -1, CurrentRating = 104 },
                new Rating { Id = 4, BaseRating = 90, SpinnakerAdjustment = -5, Adjustment = 3, CurrentRating = 88 },
                new Rating { Id = 5, BaseRating = 110, SpinnakerAdjustment = -4, Adjustment = 0, CurrentRating = 106 },
                new Rating { Id = 6, BaseRating = 98, SpinnakerAdjustment = -3, Adjustment = 1, CurrentRating = 96 },
                new Rating { Id = 7, BaseRating = 115, SpinnakerAdjustment = 0, Adjustment = -2, CurrentRating = 113 },
                new Rating { Id = 8, BaseRating = 120, SpinnakerAdjustment = -6, Adjustment = 2, CurrentRating = 116 },
                new Rating { Id = 9, BaseRating = 85, SpinnakerAdjustment = -1, Adjustment = 0, CurrentRating = 84 },
                new Rating { Id = 10, BaseRating = 92, SpinnakerAdjustment = 0, Adjustment = 1, CurrentRating = 93 },
                new Rating { Id = 11, BaseRating = 108, SpinnakerAdjustment = -3, Adjustment = -1, CurrentRating = 104 },
                new Rating { Id = 12, BaseRating = 88, SpinnakerAdjustment = -2, Adjustment = 2, CurrentRating = 88 },
                new Rating { Id = 13, BaseRating = 80, SpinnakerAdjustment = 0, Adjustment = 0, CurrentRating = 80 },
                new Rating { Id = 14, BaseRating = 112, SpinnakerAdjustment = -5, Adjustment = 1, CurrentRating = 108 },
                new Rating { Id = 15, BaseRating = 102, SpinnakerAdjustment = -4, Adjustment = 0, CurrentRating = 98 },
                new Rating { Id = 16, BaseRating = 125, SpinnakerAdjustment = 0, Adjustment = -3, CurrentRating = 122 },
                new Rating { Id = 17, BaseRating = 78, SpinnakerAdjustment = 0, Adjustment = 1, CurrentRating = 79 },
                new Rating { Id = 18, BaseRating = 82, SpinnakerAdjustment = -1, Adjustment = 0, CurrentRating = 81 },
                new Rating { Id = 19, BaseRating = 118, SpinnakerAdjustment = -5, Adjustment = 2, CurrentRating = 115 },
                new Rating { Id = 20, BaseRating = 95, SpinnakerAdjustment = -3, Adjustment = 1, CurrentRating = 93 }
            };

            context.Ratings.AddRange(ratings);
            context.Boats.AddRange(boats);
            context.SaveChanges();
        }

        public static void AddRaces(SailDBContext context)
        {
            List<Boat> boats = context.Boats.ToList();
            List<Course> courses = context.Courses.ToList();

            // Assuming we have access to the previously defined 'courses' and 'boats' lists

            var races = new List<Race>
            {
                new Race
                {
                    Id = 1,
                    StartTime = new DateTime(2024, 7, 15, 10, 0, 0), // July 15, 2024, 10:00 AM
                    EndTime = new DateTime(2024, 7, 15, 14, 0, 0),   // July 15, 2024, 2:00 PM
                    Name = "Summer Harbor Sprint",
                    Participants = new List<Boat> { boats[0], boats[1], boats[2], boats[3], boats[4] },
                    Courses = new List<Course> { courses[0] }, // Harbor Loop
                    Results = new List<Result>(),
                    Tracks = new List<Track>(),
                    RegattaId = 1
                },
                new Race
                {
                    Id = 2,
                    StartTime = new DateTime(2024, 7, 16, 9, 0, 0),  // July 16, 2024, 9:00 AM
                    EndTime = new DateTime(2024, 7, 16, 17, 0, 0),   // July 16, 2024, 5:00 PM
                    Name = "Offshore Endurance Challenge",
                    Participants = new List<Boat> { boats[5], boats[6], boats[7], boats[8], boats[9], boats[10] },
                    Courses = new List<Course> { courses[1] }, // Offshore Challenge
                    Results = new List<Result>(),
                    Tracks = new List<Track>(),
                    RegattaId = 1
                },
                new Race
                {
                    Id = 3,
                    StartTime = new DateTime(2024, 7, 17, 11, 0, 0), // July 17, 2024, 11:00 AM
                    EndTime = new DateTime(2024, 7, 17, 16, 0, 0),   // July 17, 2024, 4:00 PM
                    Name = "Island Hopper Race",
                    Participants = new List<Boat> { boats[11], boats[12], boats[13], boats[14], boats[15] },
                    Courses = new List<Course> { courses[2] }, // Island Circuit
                    Results = new List<Result>(),
                    Tracks = new List<Track>(),
                    RegattaId = 1
                },
                new Race
                {
                    Id = 4,
                    StartTime = new DateTime(2024, 7, 18, 10, 0, 0), // July 18, 2024, 10:00 AM
                    EndTime = new DateTime(2024, 7, 18, 18, 0, 0),   // July 18, 2024, 6:00 PM
                    Name = "Grand Sailing Marathon",
                    Participants = new List<Boat> { boats[16], boats[17], boats[18], boats[19], boats[0], boats[1], boats[2] },
                    Courses = new List<Course> { courses[0], courses[2], courses[1] }, // Combining all courses
                    Results = new List<Result>(),
                    Tracks = new List<Track>(),
                    RegattaId = 1
                }
            };

            context.Races.AddRange(races);
            context.SaveChanges();
        }
        public static void AddRegattas(SailDBContext context)
        {

            List<Race> races = context.Races.ToList();
            var regatta = new Regatta
            {
                Id = 1,
                Name = "Summer Sailing Series 2024",
                Races = races,
            };

            context.Regattas.Add(regatta);
            context.SaveChanges();
        }


    }
}
