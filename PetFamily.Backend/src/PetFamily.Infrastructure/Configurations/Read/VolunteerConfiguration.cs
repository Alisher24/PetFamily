using System.Text.Json;
using Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Read;

public class VolunteerConfiguration : IEntityTypeConfiguration<VolunteerDto>
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
                json => JsonSerializer.Deserialize<SocialNetworkDto[]>(json, JsonSerializerOptions.Default)!);

        //Requisite
        builder.Property(v => v.Requisites)
            .HasConversion(
                r => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<RequisiteDto[]>(json, JsonSerializerOptions.Default)!);
        
        //IsDeleted
        builder.Property(v => v.IsDeleted)
            .HasColumnName("is_deleted");
        
        builder.HasQueryFilter(v => !v.IsDeleted);
    }
}