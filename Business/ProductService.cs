using ProductMS.Business.Contracts;
using ProductMS.Data.Contracts;
using ProductMS.Data.Service.Contracts;
using ProductMS.DTOs.Products;
using ProductMS.Framework.Extensions;
using ProductMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductDataService _productDataService;

        private readonly APIDataMapper<IProduct, ProductDTO> _productMapper;
        private readonly APIDataMapper<IProduct, CreateProductRequestDTO> _createProductRequestMapper;

        public ProductService(IProductDataService productDataService,
            APIDataMapper<IProduct, ProductDTO> productMapper,
            APIDataMapper<IProduct, CreateProductRequestDTO> createProductRequestMapper

            )
        {
            _productDataService = productDataService;
            _productMapper = productMapper;
            _createProductRequestMapper = createProductRequestMapper;

        }

        public async Task<ActionStatus<ProductDTO>> CreateProduct(CreateProductRequestDTO dto)
        {
            try
            {
                IProduct result = _createProductRequestMapper.ToEntity(dto);
                ActionStatus<IProduct> data = await _productDataService.CreateProduct(result);
                if (data)
                {
                    ProductDTO response = _productMapper.ToObject(data.Result);

                    return new ActionStatus<ProductDTO>(true, response);
                }
                else if (data.HasException)
                {
                    return new ActionStatus<ProductDTO>(new ResponseVM("BPCE001"));
                }
                return new ActionStatus<ProductDTO>(data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ProductDTO>("BPC-CreatePoduct", ex);
            }
        }
    }
}
