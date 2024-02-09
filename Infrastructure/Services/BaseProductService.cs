

using Infrastructure.Dtos;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class BaseProductService(ProductService productService, PriceService priceService, ManufacturerService manufacturerService, ImageService imageService, ImageRepository imageRepository,
    ManufacturerRepository manufacturerRepository, PriceRepository priceRepository, ProductRepository productRepository, ProductImagesRepository productImagesRepository)
{
    private readonly ProductService _productService = productService;
    private readonly PriceService _priceService = priceService;
    private readonly ManufacturerService _manufacturerService = manufacturerService;
    private readonly ImageService _imageService = imageService;
    private readonly ImageRepository _imageRepository = imageRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly PriceRepository _priceRepository = priceRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly ProductImagesRepository _productImagesRepository = productImagesRepository;





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


                    var image = product.Images.FirstOrDefault();

                        var productDto = new ProductDto
                        {
                            Title = product.Title,
                            Price = Math.Round(product.Price.Price1),
                            Manufacturer = product.Manufacturer.Manufacturer1,
                            ImageUrl = image?.ImageUrl,

                        };

                        productsDtos.Add(productDto);
                    }
                }
                return productsDtos;
            }

        catch (Exception ex)
        {
            Debug.WriteLine("Error ::GetAllProductsAsync " + ex.Message);
            return null!;
        }

        

       


    }





}
