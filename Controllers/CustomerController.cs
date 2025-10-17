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
using SixLabors.ImageSharp.Formats.Pbm;

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
        return RedirectToAction("Pay", "Cart"); //cartcontroller, payaction
    }

    [HttpPost]
    public IActionResult CreateOrder(PayCartPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }
        ;
        //verifiera att session har samma cartid som pm
        bool sameCartId = SessionHelper.GetCartId(_beService).Equals(input.cartid);
        //verifiera att session har samma customerid som pm
        bool sameCustomerId = SessionHelper.GetCustomerId(_beService).Equals(input.customerid);
        //verifiera att cartid finns
        bool cartExists = _beService.DbContext.Carts.Where(e => e.Id == input.cartid).Any();
        //verifiera att customerid finns
        bool customerExists = _beService.DbContext.Customers.Where(e => e.Id == input.customerid).Any();
        //verifiera shop id från input
        bool sameShop = false;

        if (_beService.DbContext.Shops.Where(e => e.Prefix == Helpers.DomainHelper.ExtractSubDomain(_beService)).Any())
        {
            Shop shop = ShopHelper.GetCurrentShop(_beService);
            sameShop = shop.Id.Equals(input.shopid);
        }

        if (sameCartId && sameCustomerId && cartExists && customerExists && sameShop)
        {
            //create order here, show qr-code and button "pay on this device"

            //and then we redirect to view with qr code and button?!? :)
            output = RedirectToAction("finnsinteaction", "finnsintecontroller");
        }
        else
        {
            //nåt gick snett... 
            //TODO: ska vi ta bort cartitems, cart, customer o customerinfo?
            //TODO: ska vi ta bort session-värden?
            output = RedirectToAction("Index", "Home");
        }


        return output;
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