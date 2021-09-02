using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inflow.Shared.Tests
{
    public static class DbHelper
    {
        private static readonly IConfiguration Configuration = OptionsHelper.GetConfigurationRoot();

        public static DbContextOptions<T> GetOptions<T>(bool useRandomDatabaseIdentifier = true) where T : DbContext
        {
            const string databaseName = "inflow-test";
            var connectionString = Configuration["postgres:connectionString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var database = useRandomDatabaseIdentifier ? $"{databaseName}-{Guid.NewGuid():N}" : databaseName;
                connectionString = $"Host=localhost;Database={database};Username=postgres;Password=";
            }
            // else if (useRandomDatabaseIdentifier)
            // {
            //     connectionString = connectionString.Replace("{ID}", $"{Guid.NewGuid():N}");
            // }

            return new DbContextOptionsBuilder<T>()
                .UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .Options;
        }
    }
}