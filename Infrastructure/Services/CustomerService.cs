using Infrastructure.Entities;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository, ProfileRepository profileRepository, CustomerTypeRepository customerTypeRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly ProfileRepository _profileRepository = profileRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;

    public async Task<CustomerDto> CreateNewCustomer(UserRegDto userRegDto)
    {
        try
        {
            if (!await _customerRepository.ExistAsync(x => x.Email == userRegDto.Email))
            {
                var customerEntity = await _customerRepository.CreateAsync( new
                    CustomerEntity() { Email = userRegDto.Email } );

                var customerDto = new CustomerDto(
                    customerEntity.Id,
                     customerEntity.Email,
                     customerEntity.Created,
                     customerEntity.CustomerTypeId
                );
                return customerDto;
            }
            return null!;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error:: CreateTypeOfCustomer " + ex.Message);
            return null!;
        }
    }

    public async Task<CustomerDto> GetCustomerByEmailAsync(string email )
    {
        try
        {
            var customerEntity = await _customerRepository.GetOneAsync(x => x.Email ==email);
            if (customerEntity != null)
            {
                var customerDto = new CustomerDto(customerEntity.Id, customerEntity.Email, customerEntity.Created,
                    customerEntity.CustomerTypeId);
                return customerDto;
            }
            else return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetCustomerByEmailAsync " + ex.Message);
            return null!;
        }
    }
    public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync()
    {
        try
        {
            var customerEntities = await _customerRepository.GetAllAsync();
            if (customerEntities != null)
            {
                var list = new List<CustomerDto>();
                foreach (var customer in customerEntities)
                    list.Add(new CustomerDto(customer.Id, customer.Email, customer.Created, customer.CustomerTypeId));

                return list;
            }
            else { return null!; }
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetAllCustomerAsync " + ex.Message);
            return null!;
        }
    }

    public async Task<bool> UpdateCustomerEmailAsync(CustomerEntity customerEntity, UserRegDto userRegDto)
    {
        try
        {
            var customerToUpdate = await _customerRepository.GetOneAsync(x => x.Id == customerEntity.Id);
            if (customerToUpdate != null)
            {
                if (customerToUpdate.Email == userRegDto.Email)
                {
                    return true;
                }
                else
                {
                    if (customerToUpdate.Email != userRegDto.Email)
                    {
                        customerToUpdate.Email = userRegDto.Email;
                        await _customerRepository.UpdateAsync(x => x.Id == customerToUpdate.Id, customerToUpdate);
                        return true;
                    }

                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: UpdateCustomerEmailAsync " + ex.Message);
            return false!;

        }
    }
    public async Task<bool> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
        {
            try
            {
                var result = await _customerRepository.DeleteAsync(expression);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR :: DeleteCustomerTypeAsync " + ex.Message);
                return false!;

            }
        }
    }
