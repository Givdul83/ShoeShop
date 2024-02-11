

using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class BaseService_Tests
{
    private readonly CustomerDbContext _context;
    private readonly BaseService _baseService;
    private readonly CustomerTypeRepository _customerTypeRepository;
    private readonly AddressRepository _addressRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly ProfileRepository _profileRepository;
    private readonly ProfileAddressRepository _profileAddressRepository;
    private readonly AddressService _addressService;
    private readonly ProfileService _profileService;
    private readonly ProfileAddressService _profileAddressService;
    private readonly CustomerService _customerService;
    private readonly CustomerTypeService _customerTypeService;

    public BaseService_Tests()
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
        _customerTypeRepository = new CustomerTypeRepository(_context);
        _addressService = new AddressService(_addressRepository, _customerRepository, _profileRepository, _profileAddressRepository);
        _customerTypeService = new CustomerTypeService(_customerTypeRepository, _customerRepository);
        _profileService = new ProfileService(_profileRepository, _customerRepository, _profileAddressRepository);
        _customerService = new CustomerService(_customerRepository, _profileRepository, _customerTypeRepository);
        _profileAddressService = new ProfileAddressService(_addressRepository, _profileRepository, _profileAddressRepository);

        _baseService = new BaseService(_addressRepository, _profileRepository, _customerRepository, _customerTypeRepository, _profileAddressRepository
            , _addressService, _customerTypeService, _customerService, _profileService, _profileAddressService);
    }



    [Fact]

    public async Task CreateUserAsync_ShouldTakeIn_UserRegDto_CreateAllEntitiesThenReturn_True()
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
        var result = await _baseService.CreateUserAsync(userRegDto);

        //Assert
        Assert.True(result);
    }


    [Fact]

    public async Task CreateUserAsync_ShouldTakeIn_UserRegDto_ThenFailCreateAllEntitiesThenReturn_False()
    {

        //Arrange
        var userRegDto = new UserRegDto
        {
            FirstName = "test",
            LastName = "test",

            StreetName = "test",
            PostalCode = "test",
            City = "test",
            TypeOfCustomer = "test",
        };

        //Act
        var result = await _baseService.CreateUserAsync(userRegDto);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ControlUserExist_ChecksIfUserExistByEmail_AndIfFound_ReturnsTrue()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();


        //Act
        var found = await _baseService.ControlUserExistAsync(userRegDto.Email);

        //Assert
        Assert.True(found);

    }

    [Fact]
    public async Task ControlUserExist_ChecksIfUserExistByEmail_AndIfNotFound_ReturnsFalse()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();


        //Act
        var found = await _baseService.ControlUserExistAsync("Blabla@hotmail.com");

        //Assert
        Assert.False(found);

    }

    [Fact]

    public async Task DeleteUserAsync_ShouldFindANdDeleteCustomerANdProfileEntity_ByEmailAddress_ThenReturnTrue()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();

        //Act

        var result = await _baseService.DeleteUserAsync(userRegDto.Email);


        //Assert
        Assert.True(result);
    }


    [Fact]

    public async Task DeleteUserAsync_ShouldNotDeleteCustomerANdProfileEntityIfNotFound_ByEmailAddress_ThenReturnFalse()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();

        //Act

        var result = await _baseService.DeleteUserAsync("notfound@gone.com");


        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task FindUserAsync_ChecksIfUserExistByEmail_AndIfFound_ReturnsUserDto()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();


        //Act
        var found = await _baseService.FindUserAsync(userRegDto.Email);

        //Assert
        Assert.NotNull(found);
        Assert.Equal(found.FirstName, userRegDto.FirstName);

    }

    [Fact]
    public async Task FindUserAsync_ShouldCheckIfUserExistByEmail_AndIfNotFound_ReturnNull()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();


        //Act
        var found = await _baseService.FindUserAsync("notfound@gone.com");

        //Assert
        Assert.Null(found);


    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldGetallExistingUsers_Return_IEnumerable()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();



        //Act 
        var result = await _baseService.GetAllUsersAsync();

        //Assert

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<UserDto>>(result);

    }

    [Fact]
    public async Task UpdateUsersAsync_ShouldGetAllEntiteisConnectedToUserRegDto_UpdateToNewValues_ThenReturn_True()
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
        var created = await _baseService.CreateUserAsync(userRegDto);
        Assert.True(created);
        await _context.SaveChangesAsync();

        //Act

        var updatedUserRegDto = new UserRegDto
        {
            FirstName = "Updated",
            LastName = "Updated",
            Email = "test@test.com",
            StreetName = "Update",
            PostalCode = "Update",
            City = "test",
            TypeOfCustomer = "Updated"
        };

        var result = await _baseService.UpdateUser(updatedUserRegDto);
        await _context.SaveChangesAsync();
        var updated = await _baseService.FindUserAsync(userRegDto.Email);


        //Assert
        Assert.True(result);
        Assert.NotNull(updated);
        Assert.NotEqual(updated.FirstName, userRegDto.FirstName);
    }
}