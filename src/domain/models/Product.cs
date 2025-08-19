public class Product
{
    public Product(string productName, int productQuantity)
    {
        ProductId = Guid.NewGuid();
        ProductName = productName;
        ProductQuantity = productQuantity;
    }

    public Guid ProductId { get; private set; }

    public string ProductName { get; set; }

    public int ProductQuantity { get; set; }


}