using Microsoft.Extensions.DependencyInjection;
using ProductMS.Data.Contracts;
using ProductMS.DTO.Mappers.Product;
using ProductMS.DTOs.Products;
using ProductMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.DTO.Mappers
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddDTOMappers(this IServiceCollection services)
        {
            services.AddScoped<APIDataMapper<IProduct, ProductDTO>, ProductMapper>();
            services.AddScoped<APIDataMapper<IProduct, CreateProductRequestDTO>, CreateProductRequestMapper>(); 
            
            return services;
        }
    }
}
