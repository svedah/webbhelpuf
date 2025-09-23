using System;
using webbhelpuf.Helpers;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Factories;

namespace webbhelpuf.ViewModels;

public class HomeViewModel
{
    private readonly BeService _beService;

    private readonly Shop _shop;


    string _orderby;
    public List<ShopItem> ItemsBySelectedOrder
    {
        get
        {
            switch (_orderby)
            {
                case "ItemOrder":
                    return _shop.Items.OrderBy(e => e.Order).ToList();
                    break;
                case "PriceAscending":
                    return _shop.Items.OrderBy(e => e.Price).ToList();
                    break;
                case "PriceDescending":
                    return _shop.Items.OrderByDescending(e => e.Price).ToList();
                    break;
                default:
                    return _shop.Items.OrderBy(e => e.Order).ToList();
                    break;
            }
        }
    }


    public Shop Shop
    {
        get
        {
            return _shop;
        }
    }

    public string ShopTheme
    {
        get
        {
            return _shop.Settings.Theme.ToLower() ?? "standard";
        }
    }
    public string ShopLayout
    {
        get
        {
            return _shop.Settings.Layout ?? "standard";
        }
    }

    public string SubDomain
    {
        get
        {
            return Helpers.DomainHelper.ExtractSubDomain(_beService);
        }
    }

    public string QRTestURL
    {
        get
        {
            string str = "https://app.swish.nu/1/p/sw/?sw=0705501404&amt=100&cur=SEK&msg=ett%20litet%20test&src=qr";
            string estr = System.Web.HttpUtility.UrlEncode(str);
            return estr;
        }
    }

    //TODO: lägg till parameter för sorteringsordning
    public HomeViewModel(BeService beService)
    {
        _beService = beService;
        _shop = new ShopFactory(beService).BuildShopTree(SubDomain);
        _orderby = "ItemOrder";
    }
}
