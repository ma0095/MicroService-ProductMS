using Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
