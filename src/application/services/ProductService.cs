using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProduct() => await _context.Products.ToListAsync();

    public async Task<Product?> GetProductById(Guid id) => await _context.Products.FindAsync(id);

    public async Task CreateProduct(Product product)
    {
        _context.Add(product);

        await _context.SaveChangesAsync();

    }

    public async Task<Product?> IncreaseProduct(Guid productId, int value)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null) return null;

        product.ProductQuantity += value;

        _context.Products.Update(product);

        await _context.SaveChangesAsync();

        return product;

    }

    public async Task<Product?> DecreaseProduct(Guid productId, int value)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null) return null;

        if (product.ProductQuantity > 0 && product.ProductQuantity >= value)
        {
            product.ProductQuantity -= value;

            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }

        return null;



    }

    public async Task<Product?> EditProduct(Guid id, string productName)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return null;


        product.ProductName = productName;

        _context.Update(product);

        await _context.SaveChangesAsync();

        return product;

    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return false;

        _context.Remove(product);

        await _context.SaveChangesAsync();

        return true;

    }




}