﻿using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models;

public class Pet : IEntityId<Guid>
{
    public Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required string Description { get; set; }
    public required string Breed { get; set; }
    public required string Color { get; set; }
    public string? InformationHealth { get; set; }
    public required string Address { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public required string ContactPhoneNumber { get; set; }
    public bool Neutered { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public bool Vaccinated { get; set; }
    public HelpStatuses HelpStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<AssistanceDetail> AssistanceDetails { get; set; } = [];
    public required Volunteer Volunteer { get; set; }
    public long VolunteerId { get; set; }
}