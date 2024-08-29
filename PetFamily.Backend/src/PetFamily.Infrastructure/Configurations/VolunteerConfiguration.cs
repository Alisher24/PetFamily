using Domain.Models;
using Domain.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        //Id
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        //FullName
        builder.OwnsOne(v => v.FullName, vb =>
        {
            vb.ToJson("full_name");

            vb.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);

            vb.Property(f => f.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);

            vb.Property(f => f.Patronymic)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
        });

        //Email
        builder.Property(v => v.Email)
            .IsRequired()
            .HasMaxLength(Constants.MAX_EMAIL_LENTH);
        
        //Description
        builder.ComplexProperty(v => v.Description, vb =>
        {
            vb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH)
                .HasColumnName("description");
        });
        
        //YearsExperience
        builder.Property(v => v.YearsExperience)
            .IsRequired();
        
        //PhoneNumber
        builder.Property(v => v.PhoneNumber)
            .IsRequired()
            .HasMaxLength(Constants.MAX_PHONE_LENTH);

        //SocialNetworks
        builder.OwnsOne(v => v.SocialNetworks, vb =>
        {
            vb.ToJson("social_networks");

            vb.OwnsMany(l => l.SocialNetworks, lb =>
            {
                lb.OwnsOne(a => a.Name, ab =>
                {
                    ab.Property(n => n.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
                });

                lb.Property(s => s.Link)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);
            });
        });
        
        //AssistanceDetails
        builder.OwnsOne(v => v.AssistanceDetails, vb =>
        {
            vb.ToJson("assistance_details");

            vb.OwnsMany(l => l.AssistanceDetails, lb =>
            {
                lb.OwnsOne(a => a.Name, ab =>
                {
                    ab.Property(n => n.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
                });

                lb.OwnsOne(a => a.Description, ab =>
                {
                    ab.Property(d => d.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);
                });
            });
        });

        //Pets
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}