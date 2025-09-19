using System.Net.Sockets;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;

namespace webbhelpuf.Factories;



public class ShopFactory
{
    private BeService _srv;
    private ApplicationDbContext _db;

    public ShopFactory(BeService beService)
    {
        _srv = beService;
        _db = _srv.DbContext;
    }

    public Shop BuildShopTree(string domain)
    {
        Shop output;
        if (TryGetShop(domain, out output))
        {
            ;
        }
        return output;
    }

    private bool TryGetShop(string domain, out Shop shop)
    {
        bool output = false;
        if (_db.Shops.Where(e => e.Prefix == domain).Any())
        {
            ;
            shop = _db.Shops
                .Where(e => e.Prefix == domain)
                .Include(e => e.Settings)
                .Include(e => e.Settings.ContactInfo)
                .Include(e => e.Items)
                .First(e => e.Prefix == domain);
            output = true;
        }
        else
        {
            shop = CreateDummyShop();
        }
        return output;
    }

    private Shop CreateDummyShop()
    {
        Shop output = new Shop
        {
            Prefix = "_",
            Settings = new ShopSetting
            {
                Title = "Dummy",
                BaseShippingPrice = 0,
                Description = "Dummy",
                Layout = "Standard",
                Theme = "Standard",
                ContactInfo = new ShopContactInfo
                {
                    Email = "dummy@dummy.com",
                    MobileNumber = "0123456789",
                    SocialMedias = new ShopSocialMedia
                    {
                        Facebook = "https://facebook.com/",
                        Instagram = "https://instagram.com/",
                        LinkedIn = "https://linkedin.com/",
                        TikTok = "https://tiktok.com/",
                        YouTube = "https://youtube.com/"
                    }
                }
            },
            Items = new HashSet<ShopItem>()
        };
        return output;
    }

}