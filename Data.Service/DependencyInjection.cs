using Data.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IProductDataService, ProductDataService>();
            return services;
        }
    }
}
