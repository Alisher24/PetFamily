using Domain.Models;
using Domain.Models.Shared;
using Domain.Models.Species;
using Domain.Models.Species.Ids;
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
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH)
                .HasColumnName("name");
        });
        
        //Description
        builder.ComplexProperty(s => s.Description, sb =>
        {
            sb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH)
                .HasColumnName("description");
        });
        
        //Breeds
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}