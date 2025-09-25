namespace webbhelpuf.Data.Models;

public class Order
{
    public required Guid Id { get; set; }
    public required Customer Customer { get; set; }
}