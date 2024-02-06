﻿using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using UserMobileApp.ViewModels;
using UserMobileApp.Views;

namespace UserMobileApp
{
    public partial class App : Application
    {
        private static IHost? builder;


        public App()
        {
            builder = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
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
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainViewModel>();
                    services.AddTransient<UserViewModel>();
                    services.AddTransient<UserView>();
                    services.AddTransient<ProductViewModel>();
                    services.AddTransient<ProductView>();
                    services.AddTransient<StartViewModel>();
                    services.AddTransient<StartView>();

                }).Build();

        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await builder!.StartAsync();

            var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();





            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (builder!)
            {
                await builder!.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }

}