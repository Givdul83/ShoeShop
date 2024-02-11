

using Infrastructure.ProductContext;
using Infrastructure.ProductEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProductRepository_Tests
{
    private readonly ProductDbContext _context = new (new DbContextOptionsBuilder<ProductDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid}")
        .Options );

    //[Fact]
    //public async Task GetAllAsync_ReturnsAllProducts()
    //{
    //    // Arrange
    //    var productRepository = new ProductRepository( _context );
    //    var;
       

    //    // Act
    //    var products = await productRepository.GetAllAsync();

    //    // Assert
    //    Assert.NotNull(products);
    //    Assert.IsAssignableFrom<IEnumerable<Product>>(products);
    //}
}
