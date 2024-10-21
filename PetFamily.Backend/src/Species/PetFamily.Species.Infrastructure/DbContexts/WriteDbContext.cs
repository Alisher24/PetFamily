using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Options;

namespace PetFamily.Species.Infrastructure.DbContexts;

public class WriteDbContext(IConfiguration configuration) : DbContext
{
    private readonly ILoggerFactory _loggerFactory = new LoggerFactory();
    public DbSet<Domain.Species> Species => Set<Domain.Species>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DatabaseConstants.Database));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        modelBuilder.HasDefaultSchema("species");
    }
}