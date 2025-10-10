using System;
using webbhelpuf.Helpers;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Factories;

namespace webbhelpuf.ViewModels;

public class ManageViewModel
{
    private readonly BeService _beService;

    private readonly Shop _shop;


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
            return "standard";
        }
    }

    public string SubDomain
    {
        get
        {
            return Helpers.DomainHelper.ExtractSubDomain(_beService);
        }
    }


    public ManageViewModel(BeService beService)
    {
        _beService = beService;
        _shop = new ShopFactory(beService).BuildShopTree(SubDomain);
    }
}
