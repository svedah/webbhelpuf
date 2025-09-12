namespace webbhelpuf.Data.Models;

public class Shop
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    public required string HostName { get; set; }
    public required int BaseShippingPrice { get; set; }
    public required string Description { get; set; }
    public required string Theme { get; set; }

    public required User Owner { get; set; }
    public required HashSet<ShopItem> Items { get; set; }
}