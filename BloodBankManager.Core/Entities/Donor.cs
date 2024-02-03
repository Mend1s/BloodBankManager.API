using BloodBankManager.Core.Entities;
using BloodBankManager.Core.Enums;
using System.Reflection;

namespace BloodBankManager.Core.Donor;

public class Donor : BaseEntity
{
    public Donor() {}
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
}
