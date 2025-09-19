namespace webbhelpuf.Data.Models;

public class ShopSetting
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    public required int BaseShippingPrice { get; set; }
    public required string Description { get; set; }
    public required string Layout { get; set; }
    public required string Theme { get; set; }

    public required ShopContactInfo ContactInfo { get; set; }

}