public class Operation
{
    public Operation(Guid productId)
    {

    }

    public Guid OperationId { get; private set; }

    public Product OperationProduct { get; set; }

    public User OperationUser { get; set; }



}