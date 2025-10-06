using Microsoft.AspNetCore.DataProtection.Repositories;

namespace webbhelpuf.Data.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public required virtual ShopItem ShopItem { get; set; }
    // public virtual FileRepo Files { get; set; }
    public required int Amount { get; set; }

}