
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.ProductEntities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ProductService(ProductRepository productRepository)
{
    private readonly ProductRepository _productRepository = productRepository;


    public async Task<Product> CreateProductEntityAsync(ProductRegDto productRegDto)
    {
        try
        {
                var productEntity = await _productRepository.GetOneAsync(x => x.Title == productRegDto.Title);

                if (productEntity == null)
                {
                    var newproductEntity = await _productRepository.CreateAsync(new Product
                    {
                        Title = productRegDto.Title
                    });
                    if (newproductEntity != null)
                    {
                        return newproductEntity;
                    }
                    else
                    {
                        if (productEntity != null)
                            return productEntity;
                    }
                }
                return null!;
            }
        catch(Exception ex) 
        {
            Debug.WriteLine("Error CreateProductentityAsync" +ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        try
        {
            var customerEntities = await _productRepository.GetAllAsync();
            if (customerEntities != null)
            {


                return customerEntities;
            }
            else { return null!; }
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetAllCustomerAsync " + ex.Message);
            return null!;
        }
    }
}
