
using Infrastructure.Entities;
using System.Diagnostics;

namespace Infrastructure.Factories;

public static class CustomerTypeFactory
{
    public static CustomerTypeEntity CreateCustomerType(string typeOfCustomer)
    {
        try {
            var customerType = new CustomerTypeEntity { TypeOfCustomer = typeOfCustomer};
            return customerType;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR  CreateCustomerType::" +ex.Message);
        }
        return null!;
        }
}
