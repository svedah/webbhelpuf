using System;
using webbhelpuf.Helpers;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Factories;

namespace webbhelpuf.ViewModels;

public class CartViewModel
{
    private readonly BeService _beService;

    private readonly Shop _shop;

    private readonly Cart _cart;

    private readonly ShopItem _shopitem; //för article-sida

    private readonly CustomerInfo _customerInfo;

    public Shop Shop
    {
        get
        {
            return _shop;
        }
    }

    public Cart Cart
    {
        get
        {
            return _cart;
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


    //TODO: lägg till parameter för sorteringsordning
    public CartViewModel(BeService beService)
    {
        _beService = beService;
        _shop = new ShopFactory(beService).BuildShopTree(SubDomain);
        _cart = CartHelper.GetOrCreateCart(_beService);
        _customerInfo = CustomerHelper.GetOrCreateCustomer(beService).CustomerInfo;

    }
}
