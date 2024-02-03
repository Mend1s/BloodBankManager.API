using BloodBankManager.Core.Entities;

namespace BloodBankManager.Core.Donor;

public class Address : BaseEntity
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public Donor Donor { get; private set; }
}