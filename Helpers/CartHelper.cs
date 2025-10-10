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

    public static void DeleteCart(BeService _beService)
    {
        Cart cart = GetOrCreateCart(_beService);

        foreach (CartItem ci in cart.Items)
        {
            _beService.DbContext.CartItems.Remove(ci);
        }

        _beService.DbContext.Carts.Remove(cart);

        _beService.DbContext.SaveChanges();
    }

    public static void DeleteCartItems(BeService _beService)
    {
        DeleteCart(_beService);//same same...
    }

    public static bool DeleteCartItem(BeService _beService, CartItem item)
    {
        bool output = false;

        //hämta cart
        Cart cart = GetOrCreateCart(_beService);

        //cart innehåller cartitem?
        bool cartItemExists = cart.Items.Where(e => e.Id == item.Id).Any();
        if (cartItemExists)
        {
            //ta bort cartitem ur cart
            cart.Items.Remove(item);

            //ta bort cartitem ur db
            _beService.DbContext.CartItems.Remove(item);

            //spara cart
            _beService.DbContext.Carts.Update(cart);

            //TODO: ta bort cart om den är tom?

            _beService.DbContext.SaveChanges();

            output = true;
        }
        //returnera true om allt lyckades, annars false
        return output;
    }

    public static bool DeleteCartItem(BeService _beService, Guid cartItemId)
    {
        CartItem cartItem;
        if (_beService.DbContext.CartItems.Where(e => e.Id == cartItemId).Any())
        {
            cartItem = _beService.DbContext.CartItems.Where(e => e.Id == cartItemId).First();
            return DeleteCartItem(_beService, cartItem);
        }
        return false;
    }


    public static bool EditCartItem(BeService _beService, CartItem item, int amount)
    {
        bool output = false;

        //hämta cart
        Cart cart = GetOrCreateCart(_beService);

        //cart innehåller cartitem?
        bool cartItemExists = cart.Items.Where(e => e.Id == item.Id).Any();
        if (cartItemExists)
        {
            //ändra cartitem
            CartItem citem = cart.Items.Where(e => e.Id == item.Id).First();
            citem.Amount = amount;

            //spara cartitem
            _beService.DbContext.CartItems.Update(citem);
            _beService.DbContext.SaveChanges();

            output = true;
        }
        //returnera true om allt lyckades, annars false
        return output;
    }

    public static bool EditCartItem(BeService _beService, Guid cartItemId, int amount)
    {
        CartItem cartItem;
        if (_beService.DbContext.CartItems.Where(e => e.Id == cartItemId).Any())
        {
            cartItem = _beService.DbContext.CartItems.Where(e => e.Id == cartItemId).First();
            return EditCartItem(_beService, cartItem, amount);
        }
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
            ShopItem shopItem = shop.Items.Where(e => e.Id == pm.id).First();
            //create cart in db
            if (!cartInDb)
            {
                cart.Created = DateTime.Now;
                _beService.DbContext.Carts.Add(cart);
                _beService.DbContext.SaveChanges();
            }

            CartItem cartItem;
            if (cart.Items.Where(e => e.ShopItem == shopItem).Any())
            {
                //varan fanns redan i varukorg -> uppdatera mängd
                cartItem = cart.Items.Where(e => e.ShopItem == shopItem).First();
                cartItem.Amount += pm.amount;
                _beService.DbContext.Update(cartItem);
            }
            else
            {
                //varan fanns inte i varukorg -> skapa ny
                cartItem = AddCartItem(_beService, shopItem, pm.amount);
                cart.Items.Add(cartItem);
            }

            _beService.DbContext.Carts.Update(cart);
            _beService.DbContext.SaveChanges();
        }
    }
}
