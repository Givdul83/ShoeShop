using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;
public class AddressRepository_Tests
{
    private readonly CustomerDbContext _context;


    public AddressRepository_Tests()
    {
        _context = new(new DbContextOptionsBuilder<CustomerDbContext>()
       .UseInMemoryDatabase(Guid.NewGuid().ToString())
       .Options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
    [Fact]
    public async Task CreateAsync_ShouldAddAddressEntity_AndReturnAddressEntity_WithId_1()
    {
        //Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity
        {
            StreetName = "Test 27",
            PostalCode = "78963",
            City = "Testby"
        };

        //Act

        var result = await addressRepository.CreateAsync(addressEntity);

        //Assert

        Assert.NotNull(result);

        Assert.Equal(1, result.Id);
    }


    [Fact]
    public async Task CreateAsync_ShouldFailToCreateAddressEntity_AndReturn_Null()
    {
        //Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity
        {
            StreetName = "Test 27",
            PostalCode = "78963",

        };

        //Act

        var result = await addressRepository.CreateAsync(addressEntity);

        //Assert

        Assert.Null(result);
    }

    [Fact]


    public async Task GetAllAsync_ShouldReturnIEnumreableOfAddressEntities()
    {
        //Arrange
        var addressRepository = new AddressRepository(_context);


        //Act

        var result = await addressRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressEntity>>(result);

    }

    [Fact]

    public async Task GetOneAsync_ShouldReturn_OneAddressEntity_withId_1()
    {
        //Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity
        {
            StreetName = "Testgatan 7",
            PostalCode = "77777",
            City = "Testby"
        };
        await addressRepository.CreateAsync(addressEntity);
        await _context.SaveChangesAsync();
        //Act

        var result = await addressRepository.GetOneAsync(x => x.PostalCode == addressEntity.PostalCode);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(addressEntity.StreetName, result.StreetName);

    }

    [Fact]

    public async Task DeleteOneAsync_ShouldDeleteAddressEntity_AndReturnTrueOne()
    {
        //Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity
        {
            StreetName = "testgatan 4",
            PostalCode = "12222",
            City = "DeleteVille"

        };
        await addressRepository.CreateAsync(addressEntity);

        await _context.SaveChangesAsync();


        //Act

        var result = await addressRepository.DeleteAsync(x => x.Id == addressEntity.Id);

        //Assert

        Assert.True(result);

    }

    [Fact]
    public async Task DeleteOneAsync_ShouldNotFindAndDeleteAddressEntity_AndReturnFalse()
    {
        //Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity
        {
            StreetName = "testgatan 4",
            PostalCode = "12222",
            City = "Deleteville"

        };


        await _context.SaveChangesAsync();


        //Act

        var result = await addressRepository.DeleteAsync(x => x.Id == addressEntity.Id);

        //Assert

        Assert.False(result);

    }
}
