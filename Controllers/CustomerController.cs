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

public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly BeService _beService;

    public CustomerController(ILogger<CustomerController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Edit()
    {
        var vm = new CustomerViewModel(_beService);
        return View(vm);
    }

    [HttpPost]
    public IActionResult Edit(EditCustomerPostModel pm)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        Customer customer = CustomerHelper.GetOrCreateCustomer(_beService);
        if (customer.Id == Guid.Empty)
        {//not in db
            //store customer and customer info in database
            CustomerHelper.CreateCustomer(_beService, pm, ref customer);
        }
        else
        {
            CustomerHelper.EditCustomer(_beService, pm, ref customer);
        }   
            return RedirectToAction("Pay", "Cart");
    }

    public IActionResult Verify()
    {
        //hämta sessions-cart o customer-id
        //finns de inte, redirecta till home index
        
        //visa kundinfo
        //ändra kundinfo knapp

        //visa varukorg
        //ändra varukorg knapp

        //visa betala-knapp
        var vm = new CustomerViewModel(_beService);
        return View(vm);
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