
using Infrastructure.Entities;
using System.Diagnostics;

namespace Infrastructure.Factories;

public static class AddressFactory
{

    public static AddressEntity CreateAddressEntity(string streetName, string postalCode, string city)
    {
        try
        {
            var addressEntity = new AddressEntity {StreetName= streetName, PostalCode= postalCode, City= city };
            return addressEntity;

        }
        catch(Exception ex) 
        {
            Debug.WriteLine("ERROR CeateAddressEntity : :" +ex.Message);
        }
        return null!;

    } 
}
