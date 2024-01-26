
namespace Infrastructure.Dtos
{
    public record CustomerDto(Guid Id,string Email, DateTime Created, int CustomerTypeId);
    
}
