using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using webbhelpuf.Data.Models;
using webbhelpuf.Data.Seed;
using webbhelpuf.PostModels;
using webbhelpuf.Services;
using webbhelpuf.Shared;
using webbhelpuf.ViewModels;
using webbhelpuf.Helpers;
using webbhelpuf.Validators;

namespace webbhelpuf.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly BeService _beService;

    public AdminController(ILogger<AdminController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Index()
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        var vm = new AdminViewModel(_beService);
        output = View(vm);
        return output;
    }



    public bool RedirectOnDomainError(out IActionResult action)
    {
        var subdomain = Helpers.DomainHelper.ExtractSubDomain(_beService);
        bool result = false;
        if (!_beService.DbContext.Shops.Where(e => e.Prefix == subdomain).Any())
        {
            //TODO: remove port on release
            action = Redirect("//" + Constants.DEFAULTDOMAIN + "." + Constants.DOMAINNAME + ":5277/");
            result = true;
        }
        else
        {
            action = new RedirectResult("//");
            result = false;
        }
        return result;
    }

}
