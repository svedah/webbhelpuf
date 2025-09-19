using System;
using webbhelpuf.Helpers;
using webbhelpuf.Services;

namespace webbhelpuf.ViewModels;

public class WebbHelpViewModel
{
    private readonly BeService _beService;

    public WebbHelpViewModel(BeService beService)
    {
        _beService = beService;
    }
}
