using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webbhelpuf.Data.Models;
using webbhelpuf.PostModels;
using webbhelpuf.Services;
using webbhelpuf.ViewModels;

using Microsoft.AspNetCore.Authorization;

namespace webbhelpuf.Controllers;

public class TestController : Controller
{
    private readonly ILogger<TestController> _logger;
    private readonly BeService _beService;

    public TestController(ILogger<TestController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Index()
    {
        var vm = new TestViewModel(_beService);
        return View(vm);
    }

    [HttpPost]
    public IActionResult Upload(TestImagePostModel input)
    {
        ;
        MemoryStream ms = new MemoryStream();
        input.image.OpenReadStream().CopyTo(ms);
        // ImageHelper.Crop(ms);

        return RedirectToAction("Index");
    }


}
