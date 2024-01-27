
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
    services.AddSingleton<BaseService>();

}).Build();

builder.Start();

Console.ReadKey();
Console.Clear();

var customerService = builder.Services.GetRequiredService<BaseService>();

//var result = await customerService.CreateCustomer(new CustomerDtoReg
//{

//    FirstName = "Glenn",
//    LastName = "Killing",
//    Email = "Killing@gmail.com",
//    StreetName = "Illergatan 1",
//    City = "Stockholm",
//    PostalCode = "72335",
//    CustomerType = "Company"
//});



//{
//    Console.WriteLine(result ? "Operation Good" : "Failed");
//}
//Console.ReadKey();
//Console.Clear();
//var emailcustomerToDelete = "john@gmail.com";
//var delete = await customerService.DeleteCustomer(emailcustomerToDelete);

//Console.WriteLine(delete ? "customer Deleted" : "Not Deleted");

//Console.ReadKey();
Console.Clear();
//var searchedEmail = "Ambrax@gmail.com";

//var foundCustomer = await customerService.GetCustomerDto(searchedEmail);
//if (foundCustomer != null)
//{
//    Console.WriteLine("Customer found");
//    Console.WriteLine();
//    Console.Write($"First Name: {foundCustomer.FirstName}");
//    Console.WriteLine();
//    Console.Write($"Last Name: {foundCustomer.LastName}");
//    Console.WriteLine();
//    Console.Write($"Email: {foundCustomer.Email}");
//    Console.WriteLine();
//    Console.Write($"Street: {foundCustomer.StreetName}");
//    Console.WriteLine();
//    Console.Write($"PostalCode: {foundCustomer.PostalCode}");
//    Console.WriteLine();
//    Console.Write($"City: {foundCustomer.City}");
//    Console.WriteLine();
//    Console.Write($"Type of Customer: {foundCustomer.CustomerType}");
//    Console.WriteLine();
//    Console.ReadKey();
//}
//else
//{
//    Console.WriteLine("fucking failed");
//}

//var customerToUpdateEmail = "Ludde@gmail.com";

//var foundCustomerToUpdate = await customerService.FindCustomer(customerToUpdateEmail);
//var customerUpdate = new CustomerDtoReg();

//Console.WriteLine("Customer found");
//Console.WriteLine();
//Console.Write($"Current First Name: {foundCustomerToUpdate.FirstName}  Enter new First name : ");
//customerUpdate.FirstName = Console.ReadLine()!;
//Console.WriteLine();
//Console.Write($"Last Name: {foundCustomerToUpdate.LastName} Enter new Last name :");
//customerUpdate.LastName = Console.ReadLine()!;
//Console.WriteLine();
//Console.Write($"Street: {foundCustomerToUpdate.StreetName} Enter new Street name :");
//customerUpdate.StreetName = Console.ReadLine()!;
//Console.WriteLine();
//Console.Write($"PostalCode: {foundCustomerToUpdate.PostalCode} Enter new PostaCode  :");
//customerUpdate.PostalCode = Console.ReadLine()!;
//Console.WriteLine();
//Console.Write($"City: {foundCustomerToUpdate.City}  Enter new City  :");
//customerUpdate.City = Console.ReadLine()!;
//Console.WriteLine();
//Console.Write($"Type of Customer: {foundCustomerToUpdate.CustomerType} Enter new customer type :");
//customerUpdate.CustomerType = Console.ReadLine()!;
//Console.WriteLine();
//customerUpdate.Email = customerToUpdateEmail;
//Console.ReadKey();

//var updated = await customerService.UpdateCustomer(customerUpdate);
//if (updated)
//{
//    Console.WriteLine("Customer updated: {updated}");
//}
//else
//{
//    Console.WriteLine("No customer updated");
//}
//Console.ReadKey();