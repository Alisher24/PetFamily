﻿using Domain.Aggregates.Species.Entities;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);
        
        //Id
        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));
        
        //Name
        builder.ComplexProperty(b => b.Name, bb =>
        {
            bb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxLowTextLength)
                .HasColumnName("name");
        });
        
        //Description
        builder.ComplexProperty(b => b.Description, bb =>
        {
            bb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxHighTextLength)
                .HasColumnName("description");
        });
    }
}