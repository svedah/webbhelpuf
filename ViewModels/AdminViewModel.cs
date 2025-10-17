using System;
using webbhelpuf.Helpers;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Factories;

namespace webbhelpuf.ViewModels;

public class AdminViewModel
{
    private readonly BeService _beService;
    public string ShopTheme
    {
        get
        {
            return "standard";
        }
    }
    public string ShopLayout
    {
        get
        {
            return "standard";
        }
    }
    public AdminViewModel(BeService beService)
    {
        _beService = beService;
    }

}
