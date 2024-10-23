using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        //Id
        builder.HasKey(v => v.Id);

        //FullName
        builder.Property(v => v.FullName)
            .HasConversion(
                f => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<FullNameDto>(json, JsonSerializerOptions.Default)!);

        //SocialNetwork
        builder.Property(v => v.SocialNetworks)
            .HasConversion(
                s => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialNetworkDto>>(json, JsonSerializerOptions.Default)!);

        //Requisite
        builder.Property(v => v.Requisites)
            .HasConversion(
                r => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!);
        
        //Pets
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);
        
        //IsDeleted
        builder.Property(v => v.IsDeleted)
            .HasColumnName("is_deleted");
        
        builder.HasQueryFilter(v => !v.IsDeleted);
    }
}