using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
// using webbhelpuf.Models;
using webbhelpuf.Services;
using webbhelpuf.ViewModels;

namespace webbhelpuf.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BeService _beService;

    public HomeController(ILogger<HomeController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Index()
    {
        var vm = new HomeViewModel(_beService);
        return View(vm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
