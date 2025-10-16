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

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly BeService _beService;

    public CartController(ILogger<CartController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Add()
    {
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Add(AddToCartPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }
        if (!PostModelHelper.IsValid(input))
        {
            output = RedirectToAction("Add");
            return output;
        }

        Helpers.CartHelper.AddItemToCart(_beService, input);

        return RedirectToAction("Add");
    }

    public IActionResult Delete()
    {
        return RedirectToAction("Edit");
    }

    [HttpPost]
    public IActionResult Delete(DeleteCartItemPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        CartHelper.DeleteCartItem(_beService, input.id);
        return RedirectToAction("Delete");
    }

    public IActionResult Edit()
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }
        var vm = new CartViewModel(_beService);
        return View(vm);
    }

    [HttpPost]
    public IActionResult Edit(EditCartPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        CartHelper.EditCartItem(_beService, input.id, input.amount);

        return RedirectToAction("Edit");
    }

    public IActionResult Pay()
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        //Tom varukorg? => redirecta till startsidan
        bool cartIsEmpty = CartHelper.GetOrCreateCart(_beService).Items.Count == 0;
        if (cartIsEmpty)
        {
            return RedirectToAction("Index", "Home");
        }

        //2. om användaren inte har en sessioncustomer-adress, redirecta till "registrera adress"
        Customer customer = CustomerHelper.GetOrCreateCustomer(_beService);
        var sessionHasCustomerAddress = CustomerHelper.GetOrCreateCustomer(_beService).CustomerInfo is not null;
        if (!sessionHasCustomerAddress)
        {
            return RedirectToAction("Edit", "Customer");
        }

        //3. redirecta till "verifiera adress och varukorg"
        return RedirectToAction("Verify", "Customer");
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