

using Infrastructure.Dtos;
using Infrastructure.ProductEntities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ManufacturerService(ManufacturerRepository manufacturerRepository)
{
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;


    public async Task<Manufacturer> CreateManufacturerEntityAsync(ProductRegDto productRegDto)
    {
        try {
            var manufacturerEntity = await _manufacturerRepository.GetOneAsync(x => x.Manufacturer1 == productRegDto.Manufacturer);

            if (manufacturerEntity == null)
            {
                var newManufacturerEntity = await _manufacturerRepository.CreateAsync(new Manufacturer
                {
                    Manufacturer1 = productRegDto.Manufacturer
                });
                if(newManufacturerEntity != null)
                {
                    return newManufacturerEntity;
                }
                else
                {
                    if (manufacturerEntity != null)
                    return manufacturerEntity;
                }
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error CreateManufacturerEntityAsync " +ex.Message);
            return null!;
        }
        }



}
