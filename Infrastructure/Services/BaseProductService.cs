

using Infrastructure.Dtos;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class BaseProductService(ProductService productService, PriceService priceService, ManufacturerService manufacturerService, ImageService imageService, ImageRepository imageRepository, ManufacturerRepository manufacturerRepository, PriceRepository priceRepository, ProductRepository productRepository)
{
    private readonly ProductService _productService = productService;
    private readonly PriceService _priceService = priceService;
    private readonly ManufacturerService _manufacturerService = manufacturerService;
    private readonly ImageService _imageService = imageService;
    private readonly ImageRepository _imageRepository = imageRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly PriceRepository _priceRepository = priceRepository;
    private readonly ProductRepository _productRepository = productRepository;






    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        try
        {

            var productsDtos = new List<ProductDto>();

            var products = await _productService.GetAllProductsAsync();

            if (products != null)
            {
                foreach (var product in products)
                {

                    var price = await _priceRepository.GetOneAsync(ct => ct.Id == product.PriceId);
                    var manufacturer = await _manufacturerRepository.GetOneAsync(x => x.Id == product.ManufacturerId);



                    if (price != null && manufacturer != null && product.Images != null)
                    {

                        var productDto = new ProductDto
                        {
                            Title = product.Title,
                            Price = price.Price1,
                            Manufacturer = manufacturer.Manufacturer1,
                            ImageURLs = product.Images.Select(i => i.ImageUrl).ToList(),

                        };

                        productsDtos.Add(productDto);
                    }
                }
                return productsDtos;
            }



            return null!;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error ::GetAllProductsAsync " + ex.Message);
            return null!;
        }


    }





}
