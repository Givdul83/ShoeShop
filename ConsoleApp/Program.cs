
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=Customer_DB;Integrated Security=True;Trust Server Certificate=True"));


    services.AddSingleton<AddressRepository>();
    services.AddSingleton<CustomerRepository>();
    services.AddSingleton<ProfileRepository>();
    services.AddSingleton<CustomerTypeRepository>();
    services.AddSingleton<ProfileAddressRepository>();
    services.AddSingleton<CustomerService>();

}).Build();

builder.Start();

Console.ReadKey();
Console.Clear();

var customerService = builder.Services.GetRequiredService<CustomerService>();

var result= await customerService.CreateCustomerDto(new NewCustomerDto
    {

    FirstName = "Leo",
    LastName = "Bus",
    Email = "Leo@gmail.com",
    StreetName = "Hökgatan 1",
    City = "Arboga",
    PostalCode = "12345",
    CustomerType = "Company"
});



{
    Console.WriteLine(result? "Operation Good" : "Failed");
}

