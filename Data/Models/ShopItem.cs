namespace webbhelpuf.Data.Models;

public class ShopItem
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public int ItemsAvailable { get; set; } //0=outofstock
    public int Price { get; set; }
    // public int ShippingPrice { get; set; }
    public required string Description { get; set; }

    public required Shop Shop { get; set; }
    public required Image PrimaryImage { get; set; }
    public required HashSet<Image> Images { get; set; }
}