namespace webbhelpuf.Data.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public required ShopItem ShopItem { get; set; }
    public int Amount { get; set; }

}