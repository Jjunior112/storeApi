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

    public async Task AddProduct (Product product)
    {
        _context.Add(product);

        await _context.SaveChangesAsync();

    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if(product==null) return false;

        _context.Remove(product);

        await _context.SaveChangesAsync();

        return true;
        
    }

    

    
}