using System;
using webbhelpuf.Helpers;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Factories;

namespace webbhelpuf.ViewModels;

public class CustomerViewModel
{
    private readonly BeService _beService;

    private readonly Shop _shop;

    private readonly Cart _cart;

    private readonly Customer _customer;

    private readonly CustomerInfo _customerInfo;


    public bool HasAddress
    {
        get
        {
            return _customerInfo is not null && _customerInfo.Id != Guid.Empty;
        }
    }

    public Customer Customer
    {
        get
        {
            return _customer;
        }
    }

    public CustomerInfo CustomerInfo
    {
        get
        {
            return _customerInfo;
            // if (_customerInfo is not null)
            // {
            //     return _customerInfo;
            // }
            // else
            // {
            //     return new CustomerInfo
            //     {
            //         Id = Guid.Empty,
            //         FirstName = "Förnamn",
            //         LastName = "Efternamn",
            //         StreetName = "Gatan",
            //         StreetNo = "1",
            //         ZipCode = "12345",
            //         City = "Staden",
            //         Email = "epost@dressen.se",
            //         Phone = "0700123456",
            //         Info = "Information",
            //     };
            // }
        }
    }


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

    public int CartTotal
    {
        get
        {
            int output = 0;
            if (_cart is not null && _cart.Items is not null && Cart.Items.Count > 0)
            {
                foreach (CartItem cartItem in Cart.Items)
                {
                    output += cartItem.ShopItem.Price * cartItem.Amount;
                }
                // Cart.Items.Sum(e => e.Amount * e.ShopItem.Price);
            }
            return output;
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
    public CustomerViewModel(BeService beService)
    {
        _beService = beService;
        _shop = new ShopFactory(beService).BuildShopTree(SubDomain);
        _cart = CartHelper.GetOrCreateCart(_beService);
        _customer = CustomerHelper.GetOrCreateCustomer(beService);
        _customerInfo = CustomerHelper.GetOrCreateCustomer(beService).CustomerInfo;
    }
}
