using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMS.Business.Contracts;
using ProductMS.DTOs.Product;
using ProductMS.DTOs.Products;
using ProductMS.Framework.Extensions;

namespace ProductMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult> CreateProduct(CreateProductRequestDTO dto)
        {
            try
            {
                ActionStatus<ProductDTO> result = await _productService.CreateProduct(dto);
                if (result)
                {
                    result.Response = new ResponseVM("CPC0001");
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("CPCE001")));
                }
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CPCE001")));
            }
        }
        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<ActionResult> GetProductById(long id)
        {
            try
            {
                ActionStatus<ProductDTO> responsemodel = await _productService.GetProductById(id);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CPG0001");

                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {

                    return StatusCode(500, new ActionStatus(new ResponseVM("CPGE001")));
                }
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CPGE001")));
            }
        }
        [HttpPost]
        [Route("EditProduct")]
        public async Task<ActionResult> EditProduct(EditProductRequestDTO dto)
        {
            try
            {
                ActionStatus<ProductDTO> responsemodel = await _productService.EditProduct(dto);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CPE0001");
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("CPEE001")));
                }
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CPEE001")));
            }
        }
        [HttpPost]
        [Route("Test")]
        public async Task<ActionResult> Test(EditProductRequestDTO dto)
        {
           return Ok(dto);
        }
    }
}
