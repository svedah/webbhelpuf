using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class CartHelper
{
    public static bool AddCartItem(BeService _beService, ShopItem item, int amount)
    {
        return false;
    }
    public static bool DeleteCartItem(BeService _beService, CartItem item)
    {
        return false;
    }
    public static bool EditCartItem(BeService _beService, CartItem item, int amount)
    {
        return false;
    }
    public static bool DeleteCartItems(BeService _beService, CartItem item)
    {
        return false;
    }

    public static bool GetCart(BeService _beService, Guid id, out Cart cart)
    {
        bool output = _beService.DbContext.Carts.Where(e => e.Id == id).Any();
        if (output)
        {
            cart = _beService.DbContext.Carts
                    .Where(e => e.Id == id)
                    .Include(e => e.Shop)
                    .Include(e => e.Items)
                    .ThenInclude(e => e.ShopItem)
                    .First();
        }
        else
        {
            cart = new Cart
            {
                Created = DateTime.UnixEpoch,
                Items = new HashSet<CartItem>(),
                Shop = null
            };
        }
        return output;
    }

    public static List<CartItem> GetCartItemsByDomain(BeService _beService, string domain)
    {
        var output = new List<CartItem>();
        return output;
    }
}
