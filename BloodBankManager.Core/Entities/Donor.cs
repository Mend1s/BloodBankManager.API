using BloodBankManager.Core.Entities;
using BloodBankManager.Core.Enums;

namespace BloodBankManager.Core.Donor;

public class Donor : BaseEntity
{
    public Donor() { }
    public Donor(string fullName, string email, DateTime birthDate, GenderEnum gender, double weight, BloodTypeEnum bloodType, RhFactorEnum rhFactor, Address address)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Donations = new List<Donation>();
        Address = address;
        Active = true;
        CreatedAt = DateTime.Now;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public GenderEnum Gender { get; private set; }
    public double Weight { get; private set; }
    public BloodTypeEnum BloodType { get; private set; }
    public RhFactorEnum RhFactor { get; private set; }
    public List<Donation> Donations { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private const int MinimumAge = 16;
    private const int MaximumAge = 69;
    private const int MinimumWeight = 50;

    public void Update(string fullName, GenderEnum gender, double weight, Address address)
    {
        FullName = fullName;
        Gender = gender;
        Weight = weight;
        Address = address;
    }

    public void ActivateDonor()
    {
        Active = true;
    }

    public void DeactivateDonor()
    {
        Active = false;
    }

    public void AgeAvailable(DateTime birthDate)
    {
        var today = DateTime.Today;

        var age = today.Year - birthDate.Year;

        if (age < MinimumAge && age > MaximumAge)
            throw new Exception("You must have age between 16 and 69.");
    }

    public void MinimunWeight(double weight)
    {
        if (weight < MinimumWeight) throw new Exception("Você deve pesar no mínimo 50kg.");
    }

    public bool CheckDonor(Donor donor)
    {
        var today = DateTime.Today;

        var age = today.Year - donor.BirthDate.Year;
        if (donor.BirthDate.Date > today.AddYears(-age)) age--;

        if (age <= MinimumAge || age >= MaximumAge)
            throw new InvalidOperationException("A idade deve estar entre 16 e 69 anos.");

        if (donor.Weight <= MinimumWeight)
            throw new InvalidOperationException("O peso deve ser de no mínimo 50kg.");

        if (donor.Gender == GenderEnum.Female && donor.Donations.Any())
        {
            var lastDonation = donor.Donations.Max(dt => dt.DonationDate);
            var daysSinceLastDonation = (today - lastDonation).Days;

            if (daysSinceLastDonation < 90) throw new InvalidOperationException("Mulheres só podem doar a cada 90 dias.");
        }

        if (donor.Gender == GenderEnum.Male && donor.Donations.Any())
        {
            var lastDonation = donor.Donations.Max(dt => dt.DonationDate);
            var daysSinceLastDonation = (today - lastDonation).Days;

            if (daysSinceLastDonation < 60) throw new InvalidOperationException("Homens só podem doar a cada 60 dias.");
        }

        return true;
    }
}
