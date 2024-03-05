using BloodBankManager.Core.Donor;
using BloodBankManager.Core.Enums;

namespace BloodBankManager.Application.InputModels;

public class UpdateDonorInputModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    public double Weight { get; set; }
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public List<Donation> Donations { get; set; }
    public Address Address { get; set; }
}
