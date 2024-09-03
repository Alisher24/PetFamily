using Domain.Aggregates.Species;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);
        
        //Id
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));
        
        //Name
        builder.ComplexProperty(s => s.Name, sb =>
        {
            sb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxLowTextLenth)
                .HasColumnName("name");
        });
        
        //Description
        builder.ComplexProperty(s => s.Description, sb =>
        {
            sb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxHighTextLenth)
                .HasColumnName("description");
        });
        
        //Breeds
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}