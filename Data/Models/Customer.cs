namespace webbhelpuf.Data.Models;

public class Customer
{
    public Guid Id { get; set; }

    public required virtual Cart Cart { get; set; }
    public required virtual CustomerInfo CustomerInfo {get;set;}

}