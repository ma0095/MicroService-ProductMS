using ProductMS.Business.Contracts;
using ProductMS.Data.Contracts;
using ProductMS.Data.Service.Contracts;
using ProductMS.DTOs.Product;
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
        private readonly APIDataMapper<IProduct, EditProductRequestDTO> _editProductRequestMapper;

        public ProductService(IProductDataService productDataService,
            APIDataMapper<IProduct, ProductDTO> productMapper,
            APIDataMapper<IProduct, CreateProductRequestDTO> createProductRequestMapper,
            APIDataMapper<IProduct, EditProductRequestDTO> editProductRequestMapper

            )
        {
            _productDataService = productDataService;
            _productMapper = productMapper;
            _createProductRequestMapper = createProductRequestMapper;
            _editProductRequestMapper = editProductRequestMapper;

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
        public async Task<ActionStatus<ProductDTO>> GetProductById(long id)
        {
            try
            {
                ActionStatus<IProduct> product = await _productDataService.GetProductById(id);
                if (product)
                {
                    ProductDTO response = _productMapper.ToObject(product.Result);
                    return new ActionStatus<ProductDTO>(true, response);
                }
                else if (product.HasException)
                {
                    return new ActionStatus<ProductDTO>(new ResponseVM("BPGE001"));
                }
                return new ActionStatus<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ProductDTO>("BPC-GetProductById", ex);
            }
        }
        public async Task<ActionStatus<ProductDTO>> EditProduct(EditProductRequestDTO requestmodel)
        {
            try
            {
                IProduct result = _editProductRequestMapper.ToEntity(requestmodel);
                ActionStatus<IProduct> data = await _productDataService.EditProduct(result);
                if (data)
                {
                    ProductDTO Response = _productMapper.ToObject(data.Result);
                    return new ActionStatus<ProductDTO>(true, Response);
                }
                else if (data.HasException)
                {
                    return new ActionStatus<ProductDTO>(new ResponseVM("BPEE001"));
                }
                return new ActionStatus<ProductDTO>(data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ProductDTO>("BPC-EditProduct", ex);
            }
        }
    }
}
