using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using webbhelpuf.Data.Models;
using webbhelpuf.Data.Seed;
using webbhelpuf.Services;
using webbhelpuf.Shared;
using webbhelpuf.ViewModels;

namespace webbhelpuf.Controllers;

[Authorize(Roles = "Owner, Administrator")]
public class ManageController : Controller
{
    private readonly ILogger<ManageController> _logger;
    private readonly BeService _beService;

    public ManageController(ILogger<ManageController> logger, BeService beService)
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

        var vm = new ManageViewModel(_beService);
        return View(vm);
    }


}
