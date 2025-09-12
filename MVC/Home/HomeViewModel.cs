using System;
using webbhelpuf.Helpers;
using webbhelpuf.Services;

namespace webbhelpuf.ViewModels;

public class HomeViewModel
{
    private readonly BeService _beService;

    public string SubDomain
    {
        get
        {
            return Helpers.DomainHelper.ExtractHost(_beService);
        }
    }
    public HomeViewModel(BeService beService)
    {
        _beService = beService;
    }
}
