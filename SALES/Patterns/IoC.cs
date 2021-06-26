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
            services.AddTransient<EmployeeDataStore>();
            services.AddTransient<ProductDataStore>();
            services.AddTransient<SaleDateStore>();
            services.AddSingleton<ApplicationDbContext>();
            services.AddTransient<ViewModel>();

            _provider = services.BuildServiceProvider();

        }

        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
