using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.ProductContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories;

public class CustomerTypeRepository_Tests
{
    private readonly CustomerDbContext _context;


    public CustomerTypeRepository_Tests()
    {
        _context = new(new DbContextOptionsBuilder<CustomerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }


    [Fact]
    public async Task CreateAsync_ShouldAddCustomerTypeEntity_AndReturnCustomerTpeEntity_WithId_1()
    {
        //Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerTypeEntity = new CustomerTypeEntity { TypeOfCustomer = "Test" };

        //Act

        var result = await customerTypeRepository.CreateAsync(customerTypeEntity);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreateAsync_ShouldFailToCreateCustomerTypeEntity_AndReturn_Null()
    {
        //Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerTypeEntity = new CustomerTypeEntity();

        //Act

        var result = await customerTypeRepository.CreateAsync(customerTypeEntity);

        //Assert

        Assert.Null(result);
    }

    [Fact]


    public async Task GetAllAsync_ShouldReturnIEnumreableOfCustomerTypeEntities()
    {
        //Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        //Act

        var result = await customerTypeRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerTypeEntity>>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldReturn_OneCustomerTypeEntity_withId_1()
    {
        //Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerTypeEntity = new CustomerTypeEntity
        {
            TypeOfCustomer = "Test",
        };
        await customerTypeRepository.CreateAsync(customerTypeEntity);
        await _context.SaveChangesAsync();
        //Act

        var result = await customerTypeRepository.GetOneAsync(x => x.TypeOfCustomer == customerTypeEntity.TypeOfCustomer);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public async Task DeleteOneAsync_ShouldDeleteCustomerEntity_AndReturnTrue()
    {
        //Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerTypeEntity = new CustomerTypeEntity { TypeOfCustomer = "Test" };



        await customerTypeRepository.CreateAsync(customerTypeEntity);

        await _context.SaveChangesAsync();


        //Act

        var result = await customerTypeRepository.DeleteAsync(x => x.Id == customerTypeEntity.Id);

        //Assert

        Assert.True(result);

    }
    [Fact]
    public async Task DeleteOneAsync_ShouldNotFindAndDeleteCustomerTypeEntity_AndReturnFalse()
    {
        //Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerTypeEntity = new CustomerTypeEntity { TypeOfCustomer = "Test" };


        await _context.SaveChangesAsync();


        //Act

        var result = await customerTypeRepository.DeleteAsync(x => x.Id == customerTypeEntity.Id);

        //Assert

        Assert.False(result);

    }
}
