using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.Shared;
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

        //Name
        builder.ComplexProperty(p => p.Name, pb =>
        {
            pb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH)
                .HasColumnName("name");
        });
        
        //Description
        builder.ComplexProperty(p => p.Description, pb =>
        {
            pb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH)
                .HasColumnName("description");
        });
        
        //Color
        builder.Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
        
        //InformationHealth
        builder.Property(p => p.InformationHealth)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENTH);
        
        //Address
        builder.OwnsOne(p => p.Address, pb =>
        {
            pb.ToJson("address");

            pb.Property(a => a.County)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
            
            pb.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
            
            pb.Property(a => a.District)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
            
            pb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
            
            pb.Property(a => a.House)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENTH);
        });

        //Weight
        builder.Property(p => p.Weight)
            .IsRequired();
        
        //Height
        builder.Property(p => p.Height)
            .IsRequired();
        
        //PhoneNumber
        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(pn => pn.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_LENTH)
                .HasColumnName("phone_number");
        });

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
        
        //Requisites
        builder.OwnsOne(p => p.Requisites, pb =>
        {
            pb.ToJson("requisites");
            
            pb.OwnsMany(l => l.Requisites, lb =>
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
        
        //Type
        builder.ComplexProperty(p => p.Type, pb =>
        {
            pb.Property(s => s.SpeciesId)
                .HasConversion(id => id.Value,
                    value => SpeciesId.Create(value))
                .IsRequired();

            pb.Property(b => b.BreedId)
                .IsRequired();
        });
    }
}