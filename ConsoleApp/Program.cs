
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=Customer_DB;Integrated Security=True;Trust Server Certificate=True"));


    services.AddTransient<AddressRepository>();
    services.AddTransient<CustomerRepository>();
    services.AddTransient<ProfileRepository>();
    services.AddTransient<CustomerTypeRepository>();
    services.AddTransient<ProfileAddressRepository>();
    services.AddTransient<BaseService>();
    services.AddTransient<ProfileService>();
    services.AddTransient<AddressService>();    
    services.AddTransient<CustomerService>();
    services.AddTransient<CustomerTypeService>();
    services.AddTransient<ProfileAddressService>();

}).Build();

builder.Start();

Console.ReadKey();
Console.Clear();

var userService = builder.Services.GetRequiredService<BaseService>();



var result = await userService.CreateUserAsync(new UserRegDto
{

FirstName = "Leo",
LastName = "Vinoy",
Email = "Leo@gmail.com",
StreetName = "Mariavägen 1",
City = "Örebro",
PostalCode = "66665",
TypeOfCustomer = "Company"
});



{
    //    Console.WriteLine(result ? "Operation Good" : "Failed");
    //}
    //Console.ReadKey();
    Console.Clear();


    var emailExists = await userService.ControlUserExistAsync("Greger@gmail.com");

    Console.ReadKey();
    Console.Clear();

    var emailcustomerToDelete = "Ambra@gmail.com";
    var delete = await userService.DeleteUserAsync(emailcustomerToDelete);

    Console.WriteLine(delete ? "customer Deleted" : "Not Deleted");

    Console.ReadKey();


    Console.ReadKey();
    Console.Clear();

    var searchedEmail = "Ambra@gmail.com";

    var foundCustomer = await userService.FindUserAsync(searchedEmail);
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
        Console.Write($"Type of Customer: {foundCustomer.TypeOfCustomer}");
        Console.WriteLine();
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("fucking failed");
    }
    Console.ReadKey();
    Console.Clear();

    var customerList = await userService.GetAllUsersAsync();

    foreach (var user in customerList)
    {
        Console.WriteLine("Customer found");
        Console.WriteLine();
        Console.Write($"First Name: {user.FirstName}");
        Console.WriteLine();
        Console.Write($"Last Name: {user.LastName}");
        Console.WriteLine();
        Console.Write($"Email: {user.Email}");
        Console.WriteLine();

        Console.Write($"Street: {user.StreetName}");
        Console.WriteLine();
        Console.Write($"PostalCode: {user.PostalCode}");
        Console.WriteLine();
        Console.Write($"City: {user.City}");
        Console.WriteLine();

        Console.Write($"Type of Customer: {user.TypeOfCustomer}");
        Console.WriteLine();

    }
    Console.ReadKey();
    Console.Clear();


var customerToUpdateEmail = "Leo@gmail.com";

var foundCustomerToUpdate = await userService.FindUserAsync(customerToUpdateEmail);
var customerUpdate = new UserRegDto();

Console.WriteLine("Customer found");
Console.WriteLine();
Console.Write($"Current First Name: {foundCustomerToUpdate.FirstName}  Enter new First name : ");
customerUpdate.FirstName = Console.ReadLine()!;
Console.WriteLine();
Console.Write($"Last Name: {foundCustomerToUpdate.LastName} Enter new Last name :");
customerUpdate.LastName = Console.ReadLine()!;
Console.WriteLine();
Console.Write($"Street: {foundCustomerToUpdate.StreetName} Enter new Street name :");
customerUpdate.StreetName = Console.ReadLine()!;
Console.WriteLine();
Console.Write($"PostalCode: {foundCustomerToUpdate.PostalCode} Enter new PostaCode  :");
customerUpdate.PostalCode = Console.ReadLine()!;
Console.WriteLine();
Console.Write($"City: {foundCustomerToUpdate.City}  Enter new City  :");
customerUpdate.City = Console.ReadLine()!;
Console.WriteLine();
Console.Write($"Type of Customer: {foundCustomerToUpdate.TypeOfCustomer} Enter new customer type :");
customerUpdate.TypeOfCustomer = Console.ReadLine()!;
Console.WriteLine();
customerUpdate.Email = customerToUpdateEmail;
Console.ReadKey();

var updated = await userService.UpdateUser(customerUpdate);
if (updated)
{
    Console.WriteLine("Customer updated: {updated}");
}
else
{
    Console.WriteLine("No customer updated");
}
Console.ReadKey();
}