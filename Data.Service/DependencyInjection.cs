using Microsoft.Extensions.DependencyInjection;
using ProductMS.Data.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data.Service
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
