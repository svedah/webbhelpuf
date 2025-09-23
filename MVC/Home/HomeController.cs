using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using webbhelpuf.Data.Models;
using webbhelpuf.Data.Seed;
using webbhelpuf.Services;
using webbhelpuf.Shared;
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
        Seeder.SeedOnEmpty(_beService);//TODO: move to correct location

        var subdomain = Helpers.DomainHelper.ExtractSubDomain(_beService);
        if (!_beService.DbContext.Shops.Where(e => e.Prefix == subdomain).Any())
        {
            //TODO: remove port on release
            return Redirect("//" + Constants.DEFAULTDOMAIN + "." + Constants.DOMAINNAME + ":5277/");
        }

        var vm = new HomeViewModel(_beService);
        return View(vm);
    }

    // [Authorize(Roles = "Administrator")]
    // public IActionResult Admintest()
    // {
    //     var vm = new HomeViewModel(_beService);
    //     return View(vm);
    // }

    public IActionResult Cookies()
    {
        var vm = new HomeViewModel(_beService);
        return View(vm);
    }


    public IActionResult Privacy()
    {
        var vm = new HomeViewModel(_beService);
        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // public ActionResult<byte[]> GeneratePng([FromQuery(Name = "text")] string text,
    //         [FromQuery(Name = "ecc")] int? ecc, [FromQuery(Name = "border")] int? borderWidth)
    // [Route("/Home/GeneratePng/{text}")]
    // public ActionResult<byte[]> GeneratePng(string text = "")
    // {
    //     string dtext = System.Web.HttpUtility.UrlDecode(text);

    //     QrCode.Ecc[] errorCorrectionLevels = { QrCode.Ecc.Low, QrCode.Ecc.Medium, QrCode.Ecc.Quartile, QrCode.Ecc.High };
    //     int ecc = 3; //Math.Clamp(ecc ?? 1, 0, 3);
    //     int borderWidth = 1; //Math.Clamp(borderWidth ?? 3, 0, 999999);

    //     // string text = "Test";

    //     var qrCode = QrCode.EncodeText(dtext, QrCode.Ecc.High/*errorCorrectionLevels[(int)ecc]*/);
    //     byte[] png = qrCode.ToPng(20, (int)borderWidth);
    //     return new FileContentResult(png, "image/png");
    // }

}
