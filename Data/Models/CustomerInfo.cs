namespace webbhelpuf.Data.Models;

public class CustomerInfo
{
    public Guid Id { get; set; }

    public required string Addressee { get; set; }
    public required string StreetNo { get; set; }
    public required string ZipCode { get; set; }
    public required string City { get; set; }
    public required string Email { get; set; }

}