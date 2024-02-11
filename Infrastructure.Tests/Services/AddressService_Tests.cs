
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class AddressService_Tests
{

    private readonly CustomerDbContext _context;
   
    private readonly AddressRepository _addressRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly ProfileRepository _profileRepository;
    private readonly ProfileAddressRepository _profileAddressRepository;
    private readonly AddressService _addressService;
   
    public AddressService_Tests()
    {
        _context = new(new DbContextOptionsBuilder<CustomerDbContext>()
       .UseInMemoryDatabase(Guid.NewGuid().ToString())
       .Options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();


        _addressRepository = new AddressRepository(_context);
        _customerRepository = new CustomerRepository(_context);
        _profileRepository = new ProfileRepository(_context);
        _profileAddressRepository = new ProfileAddressRepository(_context);
       _addressService = new AddressService(_addressRepository, _customerRepository, _profileRepository, _profileAddressRepository);
        
    }

    [Fact]

    public async Task CreateAddressAsync_ShouldTakeInAUserDtoAndCreateNewAddressEntity()
    {
        //Arrange

        var userRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "test",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };
        //Act

        var result = await _addressService.CreateAddressAsync(userRegDto);
        
        //Assert

        Assert.NotNull(result);
        Assert.IsAssignableFrom<AddressEntity>(result);
    }


    [Fact]

    public async Task CreateAddressAsync_ShouldFailToTakeInAUserDtoAndCreateNewAddressEntity_AndReturnNull()
    {
        //Arrange

        var userRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "test",
            PostalCode = "test",
            
            TypeOfCustomer = "test",
        };
        //Act

        var result = await _addressService.CreateAddressAsync(userRegDto);

        //Assert

        Assert.Null(result);
       
    }
    [Fact]
    public async Task UpdateAddressAsync_ShouldTakeInAUserDtoAndCheckForAddressChanges_ThenUpdateAddressEntity_ThenReturnUpdatedAddressEntity()
    {
        //Arrange

        var userRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "test",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };
        await _addressService.CreateAddressAsync(userRegDto);

        var createdAddressEntity = await _addressRepository.GetOneAsync(x => x.StreetName == userRegDto.StreetName && x.PostalCode == userRegDto.PostalCode && x.City == userRegDto.City);

        var updateUserRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "prov",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };

        //Act

        var result = await _addressService.UpdateAddressAsync(updateUserRegDto);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(updateUserRegDto.StreetName, result.StreetName);
        Assert.NotEqual(result.Id, createdAddressEntity.Id);
    }

    [Fact]
    public async Task UpdateAddressAsync_ShouldTakeInAUserDtoAndCheckForAddressChanges_ThenReturnAlredyExistingAddressEntity()
    {
        //Arrange

        var userRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "test",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };
        await _addressService.CreateAddressAsync(userRegDto);

        var createdAddressEntity = await _addressRepository.GetOneAsync(x => x.StreetName == userRegDto.StreetName && x.PostalCode == userRegDto.PostalCode && x.City == userRegDto.City);

        var updateUserRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "test",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };

        //Act

        var result = await _addressService.UpdateAddressAsync(updateUserRegDto);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(updateUserRegDto.StreetName, result.StreetName);
        Assert.Equal(result.Id, createdAddressEntity.Id);
    }


    [Fact]

    public async Task GetAllAddressesAsync_ShouldGetAllAddressEntites_ThenReturnInFormOf_IEnumerableOfAddressDtos()
    {
        //Arrange
        var userRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            StreetName = "test",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };
        await _addressService.CreateAddressAsync(userRegDto);

        //Act

        var result = await _addressService.GetAllAddressesAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressDto>>(result);
    }


  
    }

