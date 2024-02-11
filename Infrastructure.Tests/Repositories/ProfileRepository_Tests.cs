using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProfileRepository_Tests
{
    private readonly CustomerDbContext _context;

    public ProfileRepository_Tests()
    {
        _context = new(new DbContextOptionsBuilder<CustomerDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

    }
    [Fact]
    public async Task CreateAsync_ShouldAddProfileEntity_AndReturnProfileEntity_WithId_1()
    {
        //Arrange
        var profileRepository = new ProfileRepository(_context);
        var profileEntity = new ProfileEntity
        {
            FirstName = "Test",
            LastName = "Person",
        };

        //Act

        var result = await profileRepository.CreateAsync(profileEntity);

        //Assert

        Assert.NotNull(result);

        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreateAsync_ShouldFailToCreateProfileEntity_AndReturnNull()
    {
        //Arrange
        var profileRepository = new ProfileRepository(_context);
        var profileEntity = new ProfileEntity
        {
            FirstName = "Test",

        };

        //Act

        var result = await profileRepository.CreateAsync(profileEntity);

        //Assert

        Assert.Null(result);
    }

    [Fact]

    public async Task GetAllAsync_ShouldReturnIEnumreableOfProfileEntities()
    {
        //Arrange
        var profileRepository = new ProfileRepository(_context);

        //Act

        var result = await profileRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProfileEntity>>(result);
    }



    [Fact]
    public async Task DeleteOneAsync_ShouldFindAndDeleteProfileEntity_AndReturnTrue()
    {
        //Arrange
        var profileRepository = new ProfileRepository(_context);
        var profileEntity = new ProfileEntity
        {
            FirstName = "Test",
            LastName = "Test",
        };

        await profileRepository.CreateAsync(profileEntity);

        await _context.SaveChangesAsync();


        //Act

        var result = await profileRepository.DeleteAsync(x => x.Id == profileEntity.Id);

        //Assert

        Assert.True(result);

    }


    [Fact]
    public async Task DeleteOneAsync_ShouldNotFindAndDeleteProfileEntity_AndReturnFalse()
    {
        //Arrange
        var profileRepository = new ProfileRepository(_context);
        var profileEntity = new ProfileEntity
        {
            FirstName = "Test",
            LastName = "Test",
        };


        await _context.SaveChangesAsync();


        //Act

        var result = await profileRepository.DeleteAsync(x => x.Id == profileEntity.Id);

        //Assert

        Assert.False(result);

    }

}
