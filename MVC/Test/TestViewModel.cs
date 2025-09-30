using System;
using webbhelpuf.Helpers;
using webbhelpuf.Services;

namespace webbhelpuf.ViewModels;

public class TestViewModel
{
    private readonly BeService _beService;

    public TestViewModel(BeService beService)
    {
        _beService = beService;
    }
}
