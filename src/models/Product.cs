public class Product 
{
    public Product(string productName)
    {
        ProductId = Guid.NewGuid();
        ProductName = productName;
    }

    public Guid ProductId {get;private set;}

    public string ProductName {get;set;}


}