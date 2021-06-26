using Microsoft.Extensions.DependencyInjection;
using SALES.Repositories;
using SALES.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.Patterns
{
    public static class IoC
    {
        private static readonly ServiceProvider _provider;

        static IoC()
        {
            var services = new ServiceCollection();
            services.AddSingleton<EmployeeDataStore>();
            services.AddSingleton<ProductDataStore>();
            services.AddSingleton<SaleDateStore>();
            services.AddScoped<ApplicationDbContext>();
            services.AddTransient<ViewModel>();

            _provider = services.BuildServiceProvider();

        }

        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
