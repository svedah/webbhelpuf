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

    public string ShopTheme
    {
        get
        {
            return _shop.Settings.Theme.ToLower();
        }
    }
    public string ShopLayout
    {
        get
        {
            return _shop.Settings.Layout;
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
    public HomeViewModel(BeService beService)
    {
        _beService = beService;
        _shop = new ShopFactory(beService).BuildShopTree(SubDomain);
    }
}
