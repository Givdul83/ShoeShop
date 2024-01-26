using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class CustomerTypeService(CustomerTypeRepository customerTypeRepository,CustomerRepository customerRepository)
{
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;

    

    public async Task<bool> CreateCustomerType(string customerType)
    {
        try
        {
            if (!await _customerTypeRepository.ExistAsync(x => x.TypeOfCustomer == customerType))
            {
                var customerTypeEntity = await _customerTypeRepository.
                    CreateAsync(new CustomerTypeEntity { TypeOfCustomer = customerType });
                if (customerTypeEntity != null)
                {
                    return true;
                }

            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error:: CreateTypeOfCustomer " + ex.Message);
            return false;
        }
    }

    public async Task<CustomerTypeDto> GetCustomerType(Expression<Func<CustomerTypeEntity, bool>> predicate)
    {
        try
        {
            var customerTypeEntity = await _customerTypeRepository.GetOneAsync(predicate);
            if (customerTypeEntity != null)
            {
                var customerTypeDto = new CustomerTypeDto(customerTypeEntity.Id, customerTypeEntity.TypeOfCustomer);
                return customerTypeDto;
            }
            else return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetCustomerTypeAsync " + ex.Message);
            return null!;
        }
    }
    public async Task<IEnumerable<CustomerTypeDto>> GetAllCustomerTypesAsync()
    {
        try
        {
            var customerTypeEntities = await _customerTypeRepository.GetAllAsync();
            if (customerTypeEntities != null)
            {
                var list = new List<CustomerTypeDto>();
                foreach (var customerTypeEntity in customerTypeEntities)
                    list.Add(new CustomerTypeDto(customerTypeEntity.Id, customerTypeEntity.TypeOfCustomer));

                return list;
            }
            else { return null!; }
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetAllCustomerTypesAsync " + ex.Message);
            return null!;
        }

    }
    public async Task<bool> UpdateCustomerTypeAsync(UserRegDto userRegDto)
    {
        try
        {
            var customerToUpdate = await _customerRepository.GetOneAsync(x => x.Email == userRegDto.Email);
            if (customerToUpdate != null)
            {

                var customerType = await _customerTypeRepository.GetOneAsync(i => i.TypeOfCustomer == userRegDto.TypeOfCustomer);

                if(customerToUpdate.CustomerTypeId == customerType.Id)
                {
                    return true;
                }
                else
                {
                    if(customerToUpdate.CustomerTypeId != customerType.Id)
                    {
                        customerToUpdate.CustomerTypeId = customerType.Id;
                        await _customerRepository.UpdateAsync(x =>x.Email ==customerToUpdate.Email, customerToUpdate);
                        return true;
                    }

                }
            } return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: UpdateCustomerTypeAsync " + ex.Message);
            return false!;

        }
    }
    

    public async Task<bool> DeleteCustomerTypeAsync(Expression<Func<CustomerTypeEntity, bool>>expression)
    {
        try
        {
            var result = await _customerTypeRepository.DeleteAsync(expression);
            return  result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: DeleteCustomerTypeAsync " + ex.Message);
            return false!;

        }
    }

}