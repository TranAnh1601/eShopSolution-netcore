using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductController : ControllerBase
    {
        //khoi tao 1 lan thi de gach duoi _pub...
        private readonly IProductService _productService;
     
        public ProductController(IProductService ProductService)
        {
            _productService = ProductService;
           
        }
      
        [HttpGet("public-paging/{languageId}")]
        public async Task<ActionResult> GetAllPaging([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(request);
            return Ok(products);
        }
        [HttpGet("{productId}/{languageId}")] 
        public async Task<IActionResult> GetById(int productId, string languageId) 
        {
            var product = await _productService.GetById(productId, languageId); 
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest();

              var product = await _productService.GetById(productId, request.LanguageId); 
            //tra ve 1 action getbyid
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
           
        }
        [HttpPut] //("{productId}")
        //[Consumes("multipart/form-data")]
        //[Authorize]
        public async Task<IActionResult> Update( [FromForm] ProductUpdateRequest request) //[FromRoute] int productId, [FromForm]
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //request.Id = productId;
            var affectedResult = await _productService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{productId}")]
        // [Authorize]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPatch("{productId}/{newPrice}")]
        // [Authorize]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _productService.UpdatePrice(productId, newPrice);
            if (isSuccessful) // true
                return Ok();

            return BadRequest();
        }
        //////abc
    }
}
