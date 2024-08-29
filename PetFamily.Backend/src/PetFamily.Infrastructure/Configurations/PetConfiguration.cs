using Domain.Models;
using Domain.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        //Id
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        //Nickname
        builder.Property(p => p.Nickname)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
        
        //Description
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);

        //Breed
        builder.Property(p => p.Breed)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
        
        //Color
        builder.Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
        
        //InformationHealth
        builder.Property(p => p.InformationHealth)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);
        
        //Address
        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);

        //Weight
        builder.Property(p => p.Weight)
            .IsRequired();
        
        //Height
        builder.Property(p => p.Height)
            .IsRequired();
        
        //ContactPhoneNumber
        builder.Property(p => p.ContactPhoneNumber)
            .IsRequired()
            .HasMaxLength(Constants.MAX_PHONE_LENTH);

        //IsNeutered
        builder.Property(p => p.IsNeutered)
            .IsRequired();
        
        //DateOfBirth
        builder.Property(p => p.DateOfBirth)
            .IsRequired();

        //HelpStatus
        builder.Property(p => p.HelpStatus)
            .IsRequired();
        
        //CreatedAt
        builder.Property(p => p.CreatedAt)
            .IsRequired();
        
        //AssistanceDetails
        builder.OwnsOne(p => p.AssistanceDetails, pb =>
        {
            pb.ToJson("assistance_details");

            pb.OwnsMany(l => l.AssistanceDetails, lb =>
            {
                lb.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);

                lb.Property(a => a.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);
            });
        });

        //PetPhotos
        builder.OwnsOne(p => p.PetPhotos, pb =>
        {
            pb.ToJson("pet_photos");

            pb.OwnsMany(ph => ph.PetPhotos, phb =>
            {
                phb.Property(h => h.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);

                phb.Property(h => h.IsMain)
                    .IsRequired();
            });
        });
    }
}