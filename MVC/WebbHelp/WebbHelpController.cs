using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.ViewModels;

using Microsoft.AspNetCore.Authorization;

namespace webbhelpuf.Controllers;

[Authorize(Roles = "Administrator")]
public class WebbHelpController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BeService _beService;

    public WebbHelpController(ILogger<HomeController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Index()
    {
        var vm = new WebbHelpViewModel(_beService);
        return View(vm);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
