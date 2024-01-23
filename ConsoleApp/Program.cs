
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

var result = await customerService.CreateCustomerDto(new NewCustomerDto
{

    FirstName = "Ludvig",
    LastName = "Vinoy",
    Email = "Ludde@gmail.com",
    StreetName = "Duvgatan 1",
    City = "Örebro",
    PostalCode = "72345",
    CustomerType = "Private"
});



{
    Console.WriteLine(result ? "Operation Good" : "Failed");
}
Console.ReadKey();
Console.Clear();
//var emailcustomerToDelete = "john@gmail.com";
//var delete = await customerService.DeleteCustomer(emailcustomerToDelete);

//Console.WriteLine(delete ? "customer Deleted" : "Not Deleted");

//Console.ReadKey();
Console.Clear();
var searchedEmail = "Ludde@gmail.com";

var foundCustomer= await customerService.FindCustomer(searchedEmail);
if (foundCustomer != null) 
    {
    Console.WriteLine("Customer found");
    Console.WriteLine();
    Console.Write($"First Name: {foundCustomer.FirstName}");
    Console.WriteLine();
    Console.Write($"Last Name: {foundCustomer.LastName}");
    Console.WriteLine();
    Console.Write($"Email: {foundCustomer.Email}");
    Console.WriteLine();
    Console.Write($"Street: {foundCustomer.StreetName}");
    Console.WriteLine();
    Console.Write($"PostalCode: {foundCustomer.PostalCode}");
    Console.WriteLine();
    Console.Write($"City: {foundCustomer.City}"); 
    Console.WriteLine();
    Console.Write($"Type of Customer: {foundCustomer.CustomerType}");
    Console.WriteLine();
    Console.ReadKey();
    }
else
{
    Console.WriteLine("fucking failed");
}



   