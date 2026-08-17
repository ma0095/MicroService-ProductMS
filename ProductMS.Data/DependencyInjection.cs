using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductMS.Data.Contracts;
using ProductMS.Data.Entities;
using ProductMS.Framework;
using ProductMS.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddEntities(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ProductMSContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            _ = services.AddTransient<IProduct, Product>();

            return services;
        }
    }
}
