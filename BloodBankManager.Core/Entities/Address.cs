using BloodBankManager.Core.Entities;

namespace BloodBankManager.Core.Donor;

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    //public Donor Donor { get; private set; }
}