using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HomeLibraryAPI.EF.Design
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\HomeLibraryAPI"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString(nameof(LibraryContext));

            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>()
                .UseSqlServer(connectionString, x => x.MigrationsAssembly("HomeLibraryAPI.EF.Design"));

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
