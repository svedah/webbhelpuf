using System;
using webbhelpuf.Helpers;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Factories;

namespace webbhelpuf.ViewModels;

public class HomeArticleViewModel
{
    private readonly BeService _beService;

    private readonly Shop _shop;

    private readonly Cart _cart;

    private readonly ShopItem _shopitem;

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

    public ShopItem ShopItem
    {
        get
        {
            return _shopitem;
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

    public HomeArticleViewModel(BeService beService, Guid shopItemId)
    {
        _beService = beService;
        _shop = new ShopFactory(beService).BuildShopTree(SubDomain);
        _cart = CartHelper.GetOrCreateCart(_beService);

        if (_shop.Items.Where(e => e.Id == shopItemId).Any())
        {
            _shopitem = _shop.Items.Where(e => e.Id == shopItemId).First();
        }
        else
        {
            _shopitem = new DataModelFactory(_beService).CreateEmptyShopItem();
        }
    }
}
