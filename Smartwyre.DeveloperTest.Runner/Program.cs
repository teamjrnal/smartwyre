using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Abstractions;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IRequestValidatorService, RequestValidatorService>();
            services.AddScoped<IAccountDataStore, AccountDataStore>(); // Added as scoped for simplicty, but depending on app infrascruture, this could be any scope.

            _serviceProvider = services.BuildServiceProvider();


            Console.WriteLine("Ready top make a payment");
        }
    }
}