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
        await _productService.CreateProduct(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);

    }

    [HttpPut("Edit/{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(Guid id, [FromBody] EditProductRequest request)
    {
        var product = await _productService.EditProduct(id, request.productName);

        if (product == null)
            return NotFound("Produto não encontrado.");

        return Ok(product);

    }

    [HttpPut("Increase/{id}")]
    [Authorize]

    public async Task<IActionResult> IncreaseProduct(Guid id, [FromBody] UpdateProductRequest request)
    {
        var product = await _productService.IncreaseProduct(id, request.value);

        if (product == null)
            return NotFound("Produto não encontrado.");

        return Ok($" {product.ProductName} alterado com sucesso!");
    }

    [HttpPut("Decrease/{id}")]
    [Authorize]

    public async Task<IActionResult> DecreaseProduct(Guid id, [FromBody] UpdateProductRequest request)
    {
        var product = await _productService.GetProductById(id);

        if (product == null)
            return NotFound("Produto não encontrado.");

        if (product.ProductQuantity < request.value)
            return BadRequest("Quantidade maior que estoque do produto!");

        await _productService.DecreaseProduct(id, request.value);

        return Ok($" {product.ProductName} alterado com sucesso!");
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _productService.DeleteProduct(id) ? NoContent() : NotFound();
    }
}