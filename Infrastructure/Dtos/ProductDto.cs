
namespace Infrastructure.Dtos
{
    public class ProductDto
    {
       
            public string Title { get; set; } = null!;

            public string Manufacturer { get; set; } = null!;

            public decimal Price { get; set; }

            public string ImageUrl { get; set; } = null!;
            
            public List<string> ImageURLs { get; set; } = new List<string>();

    }
}
