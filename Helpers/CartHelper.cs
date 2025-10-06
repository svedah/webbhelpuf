using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data.Models;
using webbhelpuf.Factories;
using webbhelpuf.PostModels;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class CartHelper
{
    public static CartItem AddCartItem(BeService _beService, ShopItem shopItem, int amount)
    {
        var cartItem = new CartItem
        {
            ShopItem = shopItem,
            Amount = Math.Max(1, amount)
        };
        //TODO: find cart in session and db. if NOT in db, create it.
        _beService.DbContext.CartItems.Add(cartItem);
        _beService.DbContext.SaveChanges();
        return cartItem;
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

    // should NOT create cart in db, just a dummy cart to satisfy the view
    public static Cart GetOrCreateCart(BeService _beService)
    {
        Cart output;
        Guid cartId = Guid.Empty;

        // if no cart id in session, set new cart id in session
        if (!SessionHelper.HasCartId(_beService))
        {
            cartId = Guid.NewGuid();
            SessionHelper.SetCartId(_beService, cartId);
        }
        else
        {
            cartId = SessionHelper.GetCartId(_beService);
        }

        // if no cart found, make new cart
        if (!GetCart(_beService, cartId, out output))
        {
            var domain = DomainHelper.ExtractSubDomain(_beService);
            var shop = ShopHelper.GetCurrentShop(_beService);

            output.Id = cartId;
            output.Shop = shop;
            // output.Created = DateTime.Now;
            output.Items = new HashSet<CartItem>();

            // _beService.DbContext.Carts.Add(output);
            // _beService.DbContext.SaveChanges();
        }

        //if cart.shop != shop, empty cart and set current shop
        Shop currentShop = ShopHelper.GetCurrentShop(_beService);
        if (!currentShop.Equals(output.Shop))
        {
            output.Shop = currentShop;
            output.Items = new HashSet<CartItem>();
            // _beService.DbContext.Carts.Update(output);
            // _beService.DbContext.SaveChanges();
        }

        return output;
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
                Shop = ShopHelper.GetCurrentShop(_beService)
            };
        }
        return output;
    }

    public static List<CartItem> GetCartItemsByDomain(BeService _beService, string domain)
    {
        var output = new List<CartItem>();
        return output;
    }

    public static void AddItemToCart(BeService _beService, AddToCartPostModel pm)
    {
        //fetch cart
        var cart = GetOrCreateCart(_beService);
        bool cartInDb = cart.Created != DateTime.UnixEpoch;

        //fetch shop
        Shop shop = ShopHelper.GetCurrentShop(_beService);

        //fetch shop item
        if (_beService.DbContext.ShopItems.Where(e => e.Id == pm.id).Any())
        {
            //create cart in db
            if (!cartInDb)
            {
                cart.Created = DateTime.Now;
                _beService.DbContext.Carts.Add(cart);
                _beService.DbContext.SaveChanges();
            }

            //TODO: om varan redan finns, ändra antal i befintlig cartitem

            ShopItem shopItem = shop.Items.Where(e => e.Id == pm.id).First();
            var cartItem = AddCartItem(_beService, shopItem, pm.amount);
            cart.Items.Add(cartItem);
            _beService.DbContext.Carts.Update(cart);
            _beService.DbContext.SaveChanges();
        }
    }


}
