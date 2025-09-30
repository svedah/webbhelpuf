using System.Net.Sockets;
using Microsoft.AspNetCore.Components;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;

namespace webbhelpuf.Factories;



public class DataModelFactory
{
    private BeService _srv;
    // private ApplicationDbContext _db;

    public DataModelFactory(BeService beService)
    {
        _srv = beService;
    }

    public Cart CreateCart(Shop shop)
    {
        var id = Guid.NewGuid();

        var cart = new Cart
        {
            Id = id,
            Created = DateTime.UtcNow,
            Shop = shop,
            Items = new HashSet<CartItem>()
        };

        _srv.DbContext.Carts.Add(cart);
        _srv.DbContext.SaveChanges();

        return _srv.DbContext.Carts.Where(e => e.Id == id).First();
    }

    public CartItem CreateCartItem(ShopItem item, int amount, Cart cart)
    {
        var id = Guid.NewGuid();

        var cartitem = new CartItem
        {
            Id = id,
            ShopItem = item,
            Amount = amount
        };

        _srv.DbContext.CartItems.Add(cartitem);

        var newcartitem = _srv.DbContext.CartItems.Where(e => e.Id == id).First();
        cart.Items.Add(newcartitem);
        _srv.DbContext.SaveChanges();

        return newcartitem;
    }

    public Customer CreateCustomer(Cart cart, CustomerInfo customerInfo)
    {
        var id = Guid.NewGuid();

        var customer = new Customer
        {
            Id = id,
            Cart = cart,
            CustomerInfo = customerInfo,
            CustomerState = webbhelpuf.Enums.CustomerStateEnum.test
        };

        _srv.DbContext.Customers.Add(customer);
        _srv.DbContext.SaveChanges();

        var newCustomer = _srv.DbContext.Customers.Where(e => e.Id == id).First();
        return newCustomer;
    }

    public CustomerInfo CreateCustomerInfo
    (
        string addressee,
        string streetno,
        string zipcode,
        string city,
        string email
    )
    {
        var id = Guid.NewGuid();
        var customerinfo = new CustomerInfo
        {
            Id = id,
            Addressee = addressee,
            StreetNo = streetno,
            ZipCode = zipcode,
            City = city,
            Email = email
        };

        _srv.DbContext.CustomerInfos.Add(customerinfo);
        _srv.DbContext.SaveChanges();

        var newCustomerInfo = _srv.DbContext.CustomerInfos.Where(e => e.Id == id).First();
        return newCustomerInfo;
    }


    public webbhelpuf.Data.Models.Image CreateImage(string filename, string alttext)
    {
        var id = Guid.NewGuid();
        var image = new webbhelpuf.Data.Models.Image
        {
            Id = id,
            Filename = filename,
            AltText = alttext
        };

        _srv.DbContext.Images.Add(image);
        _srv.DbContext.SaveChanges();

        var newImage = _srv.DbContext.Images.Where(e => e.Id == id).First();
        return newImage;
    }

    public static webbhelpuf.Data.Models.Image CreateDummyImage()
    {
        return new Image
        {
            Id = Guid.Empty,
            Filename = string.Empty,
            AltText = string.Empty
        };
    }


    public Shop CreateShop(string prefix, ShopSetting shopSettings)
    {
        var id = Guid.NewGuid();
        var shop = new Shop
        {
            Id = id,
            Prefix = prefix,
            Settings = shopSettings,
            Items = new HashSet<ShopItem>(),
            Orders = new HashSet<Order>()
        };

        _srv.DbContext.Shops.Add(shop);
        _srv.DbContext.SaveChanges();

        var newShop = _srv.DbContext.Shops.Where(e => e.Id == id).First();
        return newShop;
    }

    public ShopContactInfo CreateShopContactInfo(string email, string mobilenumber, ShopSocialMedia socialmedia)
    {
        var id = Guid.NewGuid();
        var shopcontactinfo = new ShopContactInfo
        {
            Email = email,
            MobileNumber = mobilenumber ?? "",
            SocialMedias = socialmedia
        };

        _srv.DbContext.ShopContactInfos.Add(shopcontactinfo);
        _srv.DbContext.SaveChanges();

        var newShopContactInfo = _srv.DbContext.ShopContactInfos.Where(e => e.Id == id).First();
        return newShopContactInfo;
    }

    public ShopItem CreateEmptyShopItem()
    {
        return new ShopItem
        {
            Id = Guid.Empty,
            Title = string.Empty,
            ItemsAvailable = 0,
            Price = 0,
            Description = string.Empty,
            Order = 0,
            Shop = null,
            PrimaryImage = null,
            Images = null
        };
    }

    public ShopItem CreateShopItem
    (
        string title,
        int itemsavailable,
        int price,
        string description,
        Shop shop,
        Image primaryimage,
        IEnumerable<Image> images
    )
    {
        var id = Guid.NewGuid();

        var shopitem = new ShopItem
        {
            Title = title ?? "",
            ItemsAvailable = Math.Max(1, itemsavailable),
            Price = Math.Max(1, price),
            Description = description ?? "",
            Order = 1,
            Shop = shop,
            PrimaryImage = primaryimage,
            Images = new HashSet<Image>(images)
        };

        _srv.DbContext.ShopItems.Add(shopitem);
        _srv.DbContext.SaveChanges();

        var newShopItem = _srv.DbContext.ShopItems.Where(e => e.Id == id).First();
        return newShopItem;
    }

    public ShopSetting CreateShopSetting
    (
        string title,
        int baseshippingprice,
        string description,
        string layout,
        string theme,
        ShopContactInfo shopcontactinfo,
        Image logoimage
    )
    {
        var id = Guid.NewGuid();

        var shopsetting = new ShopSetting
        {
            Id = id,
            Title = title,
            SwishNumber = "+461234567890",
            BaseShippingPrice = Math.Max(0, baseshippingprice),
            Description = description,
            Theme = theme ?? "standard",
            Layout = layout ?? "standard",
            LogoImage = logoimage,
            ContactInfo = shopcontactinfo
        };

        _srv.DbContext.ShopSettings.Add(shopsetting);
        _srv.DbContext.SaveChanges();

        var newShopSetting = _srv.DbContext.ShopSettings.Where(e => e.Id == id).First();
        return newShopSetting;
    }

    public ShopSocialMedia CreateShopSocialMedia
    (
        string facebook,
        string instagram,
        string linkedin,
        string tiktok,
        string youtube
    )
    {
        var id = Guid.NewGuid();

        var shopsocialmedia = new ShopSocialMedia
        {
            Id = id,
            Facebook = facebook ?? "",
            Instagram = instagram ?? "",
            LinkedIn = linkedin ?? "",
            TikTok = tiktok ?? "",
            YouTube = youtube ?? ""
        };

        _srv.DbContext.ShopSocialMedias.Add(shopsocialmedia);
        _srv.DbContext.SaveChanges();

        var newShopSocialMedia = _srv.DbContext.ShopSocialMedias.Where(e => e.Id == id).First();
        return newShopSocialMedia;
    }

}
