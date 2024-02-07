
using Infrastructure.Dtos;
using Infrastructure.ProductEntities;
using Infrastructure.Repositories;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace Infrastructure.Services;

public class PriceService(PriceRepository priceRepository, ProductRepository productRepository)
{
    private readonly PriceRepository _priceRepository = priceRepository;
    private readonly ProductRepository _productRepository = productRepository;




    public async Task<Price> CreatePriceEntityAsync(ProductRegDto productRegDto)
    {
        try
        {

            var priceEntity = await _priceRepository.GetOneAsync(x => x.Price1 == productRegDto.Price);

            if (priceEntity == null)
            {
                var newPriceEntity = await _priceRepository.CreateAsync(new Price
                {
                    Price1 = productRegDto.Price
                });
                if (newPriceEntity != null)
                {
                    return newPriceEntity;
                }
                else
                {
                    if (priceEntity != null)
                        return priceEntity;
                }
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error CreatePriceEntity " + ex.Message);
            return null!;
        }
    }

    public async Task<bool> UpdatePriceAsync(ProductRegDto productRegDto)
    {
        try
        {
            var productToUpdate = await _productRepository.GetOneAsync(x => x.Title == productRegDto.Title);
            if (productToUpdate != null)
            {

                var priceEntity = await _priceRepository.GetOneAsync(i => i.Price1 == productRegDto.Price);

                if (productToUpdate.PriceId == priceEntity.Id)
                {
                    return true;
                }
                else
                {
                    if (productToUpdate.PriceId != priceEntity.Id)
                    {
                        productToUpdate.PriceId = priceEntity.Id;
                        await _productRepository.UpdateAsync(x => x.Title == productToUpdate.Title, productToUpdate);
                        return true;
                    }

                }
            }

            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: UpdatePriceAsync " + ex.Message);
            return false!;

        }
    }

}


