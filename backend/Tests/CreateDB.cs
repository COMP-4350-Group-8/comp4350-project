using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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
    }
}
