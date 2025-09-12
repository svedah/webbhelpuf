namespace webbhelpuf.Data.Models;

public class Customer
{
    public Guid Id { get; set; }

    public required Cart Cart { get; set; }
    public required CustomerInfo CustomerInfo {get;set;}

}