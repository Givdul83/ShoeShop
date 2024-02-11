

using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class CustomerRepository_Tests
{
    private readonly CustomerDbContext _context;


    public CustomerRepository_Tests()
    {
        _context = new(new DbContextOptionsBuilder<CustomerDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

    }
    [Fact]
    public async Task CreateAsync_ShouldAddCustomerEntity_AndReturnCustomerEntity_WithEmail()
    {
        //Arrange
        var customerRepository = new CustomerRepository(_context);
        var customerEntity = new CustomerEntity { Email = "test@t.com" };

        //Act

        var result = await customerRepository.CreateAsync(customerEntity);

        //Assert

        Assert.NotNull(result);

        Assert.Equal("test@t.com", result.Email);
    }

    [Fact]
    public async Task CreateAsync_ShouldFailToCreateCustomerEntity_AndReturn_Null()
    {
        //Arrange
        var customerRepository = new CustomerRepository(_context);
        var customerEntity = new CustomerEntity();

        //Act

        var result = await customerRepository.CreateAsync(customerEntity);

        //Assert

        Assert.Null(result);
    }

    [Fact]


    public async Task GetAllAsync_ShouldReturnIEnumreableOfCustomerEntities()
    {
        //Arrange
        var customerRepository = new CustomerRepository(_context);

        //Act

        var result = await customerRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
    }

    [Fact]

    public async Task DeleteOneAsync_ShouldDeleteOneCustomerEntity_AndReturnTrueOne()
    {
        //Arrange
        var customerRepository = new CustomerRepository(_context);
        var customerEntity = new CustomerEntity
        {
            Email ="test@test.se",
            CustomerTypeId = 1 ,
            
        };
        await customerRepository.CreateAsync(customerEntity);
      
        await _context.SaveChangesAsync();


        //Act

        var result = await customerRepository.DeleteAsync(x => x.Email == customerEntity.Email);

        //Assert

        Assert.True(result);
        
    }
    [Fact]
    public async Task DeleteOneAsync_ShouldNotFindOneCustomerEntityANdeRemoveIt_AndReturnFalse()
    {
        //Arrange
        var customerRepository = new CustomerRepository(_context);
        var customerEntity = new CustomerEntity
        {
            Email = "test@test.se",
            CustomerTypeId = 1,

        };
        

        await _context.SaveChangesAsync();


        //Act

        var result = await customerRepository.DeleteAsync(x => x.Email == customerEntity.Email);

        //Assert

        Assert.False(result);

    }
}