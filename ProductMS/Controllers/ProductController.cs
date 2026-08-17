using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMS.Business.Contracts;
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
                return BadRequest(new ActionStatus(result.Response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CPCE001")));
            }
        }
    }
}
