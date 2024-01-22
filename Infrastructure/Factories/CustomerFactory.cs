
using Infrastructure.Entities;
using System.Diagnostics;

namespace Infrastructure.Factories
{
    public static class CustomerFactory
    {

        public static CustomerEntity CreateCustomerEntity(string email)
        {
            try
            {
                var customerEntity = new CustomerEntity { Email = email };
                return customerEntity;
            }
            catch (Exception ex)
            {
                {
                    Debug.WriteLine("ERROR :: CreateCustomerEntity" + ex.Message);
                }
                return null!;
            }
        }
    }
}