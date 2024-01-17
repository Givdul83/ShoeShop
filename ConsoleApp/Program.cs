
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var bulider = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=Customer_DB;Integrated Security=True;Trust Server Certificate=True"));


});

bulider.Build();