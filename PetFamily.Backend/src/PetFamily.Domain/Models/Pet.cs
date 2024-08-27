using Domain.Enums;
using Domain.Models.Base;
using Domain.Models.Shared;

namespace Domain.Models;

public class Pet : Entity<Guid>
{
    private readonly List<AssistanceDetail> _assistanceDetails = [];
    private readonly List<PetPhoto> _petPhotos = [];
    public string Nickname { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Breed { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string InformationHealth { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string ContactPhoneNumber { get; private set; } = default!;
    public bool IsNeutered { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public bool IsVaccinated { get; private set; }
    public HelpStatuses HelpStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public IReadOnlyList<AssistanceDetail> AssistanceDetails => _assistanceDetails;
    
    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

    public void AddAssistanceDetail(AssistanceDetail assistanceDetail)
        => _assistanceDetails.Add(assistanceDetail);

    public void AddPetPhoto(PetPhoto petPhoto) 
        => _petPhotos.Add(petPhoto);
    
    // ef core
    private Pet(Guid id) : base(id) { }

    public Pet(Guid id,
        string nickname,
        string description,
        string breed,
        string color,
        string informationHealth,
        string address,
        double weight,
        double height,
        string contactPhoneNumber,
        bool isNeutered,
        DateOnly dateOfBirth,
        bool isVaccinated,
        HelpStatuses helpStatus) : base(id)
    {
        Nickname = nickname;
        Description = description;
        Breed = breed;
        Color = color;
        InformationHealth = informationHealth;
        Address = address;
        Weight = weight;
        Height = height;
        ContactPhoneNumber = contactPhoneNumber;
        IsNeutered = isNeutered;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreatedAt = DateTime.Now;
    }
}