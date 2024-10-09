using System.Text.Json;
using Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        //Id
        builder.HasKey(p => p.Id);

        //SpeciesId
        builder.Property(p => p.SpeciesId)
            .HasColumnName("type_species_id");

        //BreedId
        builder.Property(p => p.BreedId)
            .HasColumnName("type_breed_id");

        //Address
        builder.Property(p => p.Address)
            .HasConversion(
                a => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<AddressDto>(json, JsonSerializerOptions.Default)!);

        //Requisites
        builder.Property(p => p.Requisites)
            .HasConversion(
                r => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<RequisiteDto>>(json, JsonSerializerOptions.Default)!);

        //Requisites
        builder.Property(p => p.PetPhotos)
            .HasConversion(
                p => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<PetPhotoDto>>(json, JsonSerializerOptions.Default)!);

        //IsDeleted
        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted");

        builder.HasQueryFilter(v => !v.IsDeleted);
    }
}