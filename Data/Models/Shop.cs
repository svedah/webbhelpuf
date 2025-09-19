namespace webbhelpuf.Data.Models;

public class Shop
{
    public Guid Id { get; set; }

    // public required virtual ShopOwner Owner { get; set; }
    public required string Prefix { get; set; }//hostname
    public required virtual ShopSetting Settings { get; set; }
    public required virtual HashSet<ShopItem> Items { get; set; }
}