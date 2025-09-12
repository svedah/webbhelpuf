namespace webbhelpuf.Data.Models;

public class CustomerInfo
{
    public Guid Id { get; set; }

    public required string FullName { get; set; }
    public required string Address { get; set; }
    public required string PostNr { get; set; }
    public required string City { get; set; }
    public required string Email {get;set;}

}