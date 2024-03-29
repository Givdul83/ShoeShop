﻿
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ProfileService(ProfileRepository profileRepository, CustomerRepository customerRepository, ProfileAddressRepository profileAddressRepository)
{
    private readonly ProfileRepository _profileRepository = profileRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly ProfileAddressRepository _profileAddressRepository = profileAddressRepository;


    public async Task<ProfileEntity> CreateNewPofile(Guid customerId, string firstName, string lastName)
    {
        try
        {
            var customer = await _customerRepository.GetOneAsync(x => x.Id == customerId);
            if (customer != null)
            {
                var profile = await _profileRepository.CreateAsync(new ProfileEntity
                {
                    CustomerId = customer.Id,
                    FirstName = firstName,
                    LastName = lastName
                });
            
            if (profile != null)
            {
                return profile;

                
            }
            
        }
            return null!;
            }

        catch (Exception ex)
        {
            Debug.WriteLine("Error:: CreateTypeOfCustomer " + ex.Message);
            return null!;
        }
    }

    public async Task<ProfileDto> GetProfileByEmailAsync(string email)
    {
        try
        {
            var customerEntity = await _customerRepository.GetOneAsync(x => x.Email == email);
            if (customerEntity != null)
            {
                var profileEntity = await _profileRepository.GetOneAsync(x => x.CustomerId == customerEntity.Id);

                if (profileEntity != null)
                {
                    var profileDto = new ProfileDto(profileEntity.FirstName, profileEntity.LastName, profileEntity.Id, profileEntity.CustomerId);

                    return profileDto;
                }
                return null!;
            }
            else return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetProfileByEmailAsync " + ex.Message);
            return null!;
        }
    }
    public async Task<IEnumerable<ProfileDto>> GetAllProfilesAsync()
    {
        try
        {
            var profileEntities = await _profileRepository.GetAllAsync();
            if (profileEntities != null)
            {
                var list = new List<ProfileDto>();
                foreach (var profile in profileEntities)
                    list.Add(new ProfileDto(profile.FirstName, profile.LastName, profile.Id, profile.CustomerId));

                return list;
            }
            else { return null!; }
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetAllProfilesAsync " + ex.Message);
            return null!;
        }
    }
    public async Task<ProfileEntity> UpdateProfileAsync(UserRegDto userRegDto)
    {
        try
        {
            var customerToupdate = await _customerRepository.GetOneAsync(x => x.Email == userRegDto.Email);
            if (customerToupdate != null)
            {

                var profileToUpdate = await _profileRepository.GetOneAsync(p => p.CustomerId == customerToupdate.Id);

                if (profileToUpdate.FirstName == userRegDto.FirstName && profileToUpdate.LastName == userRegDto.LastName)
                {
                    var notUpdated= await _profileRepository.UpdateAsync(p => p.CustomerId == customerToupdate.Id, profileToUpdate);
                    Console.WriteLine("No changes to Profile detected");
                    return notUpdated;
                }
                else
                {
                    if (profileToUpdate.FirstName != userRegDto.FirstName || profileToUpdate.LastName != userRegDto.LastName)
                    {
                        profileToUpdate.FirstName = userRegDto.FirstName;
                        profileToUpdate.LastName = userRegDto.LastName;


                       var updated = await _profileRepository.UpdateAsync(p => p.CustomerId == customerToupdate.Id, profileToUpdate);
                        Console.WriteLine("Profile Updated");
                        return updated;
                    }

                }
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: UpdateProfileAsync " + ex.Message);
            return null!;

        }
    }


    public async Task<bool> DeleteProfileAsync(Expression<Func<ProfileEntity, bool>> expression)
    {
        try
        {
            var result = await _profileRepository.DeleteAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: DeleteProfileTypeAsync " + ex.Message);
            return false!;

        }
    }
}