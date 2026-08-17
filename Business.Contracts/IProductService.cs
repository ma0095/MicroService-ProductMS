using ProductMS.DTOs.Products;
using ProductMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Business.Contracts
{
    public interface IProductService
    {
        Task<ActionStatus<ProductDTO>> CreateProduct(CreateProductRequestDTO dto);

    }
}
