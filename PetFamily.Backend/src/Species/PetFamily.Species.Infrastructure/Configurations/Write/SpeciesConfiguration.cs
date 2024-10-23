using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects.Ids;

namespace PetFamily.Species.Infrastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Domain.Species>
{
    public void Configure(EntityTypeBuilder<Domain.Species> builder)
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
                .HasMaxLength(Constants.MaxLowTextLength)
                .HasColumnName("name");
        });
        
        //Description
        builder.ComplexProperty(s => s.Description, sb =>
        {
            sb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxHighTextLength)
                .HasColumnName("description");
        });
        
        //Breeds
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}