using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Options;
using PetFamily.Volunteers.Domain;

namespace PetFamily.Volunteers.Infrastructure.DbContexts;

public class WriteDbContext(IConfiguration configuration) : DbContext
{
    private readonly ILoggerFactory _loggerFactory = new LoggerFactory();
    
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

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
        
        modelBuilder.HasDefaultSchema("volunteers");
    }
}