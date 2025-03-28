using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllProduct());

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductById(id);

        return product == null ? NotFound() : Ok(product);

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Product product)
    {
        await _productService.AddProduct(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);

    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _productService.DeleteProduct(id) ? NoContent() : NotFound();
    }
}