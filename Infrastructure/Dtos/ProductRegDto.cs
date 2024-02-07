
namespace Infrastructure.Dtos;

public class ProductRegDto
{
    public string Title { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string ImageURL { get; set; } = null!;

    public decimal Price { get; set; }
}
