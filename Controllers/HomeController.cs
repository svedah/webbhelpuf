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

        // var subdomain = Helpers.DomainHelper.ExtractSubDomain(_beService);
        // if (!_beService.DbContext.Shops.Where(e => e.Prefix == subdomain).Any())
        // {
        //     //TODO: remove port on release
        //     return Redirect("//" + Constants.DEFAULTDOMAIN + "." + Constants.DOMAINNAME + ":5277/");
        // }
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        var vm = new HomeViewModel(_beService);
        output = View(vm);
        return output;
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

    public IActionResult AddToCart()
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddToCart(AddToCartPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }
        if (!PostModelHelper.IsValid(input))
        {
            output = RedirectToAction("AddToCart");
            return output;
        }

        Helpers.CartHelper.AddItemToCart(_beService, input);

        return RedirectToAction("AddToCart");
    }


    public IActionResult Article([FromRoute] Guid Id)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }
        if (Id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }
        var vm = new HomeArticleViewModel(_beService, Id);
        return View(vm);
    }

    public IActionResult DeleteCartItem()
    {
        return RedirectToAction("EditCart");
    }

    [HttpPost]
    public IActionResult DeleteCartItem(DeleteCartItemPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        CartHelper.DeleteCartItem(_beService, input.id);

        return RedirectToAction("DeleteCartItem");
    }



    public IActionResult EditCart()
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }
        var vm = new HomeViewModel(_beService);
        return View(vm);
    }

    public IActionResult EditCartItem()
    {
        return RedirectToAction("EditCart");
    }

    [HttpPost]
    public IActionResult EditCartItem(EditCartItemPostModel input)
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        CartHelper.EditCartItem(_beService, input.id, input.amount);

        return RedirectToAction("EditCartItem");
    }

    public IActionResult PayCart()
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
            return RedirectToAction("Index");
        }
;
        //2. om användaren inte har en sessioncustomer-adress, redirecta till "registrera adress"
        var sessionHasCustomerAddress = CustomerHelper.GetOrCreateCustomer(_beService).CustomerInfo is not null;
        if (!sessionHasCustomerAddress)
        {
            return RedirectToAction("EditAddress");
        }

        //3. redirecta till "verifiera adress och varukorg"
        return RedirectToAction("VerifyAddressAndCart");

    }

    public IActionResult EditAddress()
    {
        IActionResult output;
        if (RedirectOnDomainError(out output))
        {
            return output;
        }

        //Tom varukorg? => redirecta till startsidan
        // bool cartIsEmpty = CartHelper.GetOrCreateCart(_beService).Items.Count == 0;
        // if (cartIsEmpty)
        // {
        //     return RedirectToAction("Index");
        // }

        //Om användaren har en sessioncustomer-adress, redirecta till "paycart"
        // var sessionHasCustomerAddress = CustomerHelper.GetOrCreateCustomer(_beService).CustomerInfo is not null;
        // if (sessionHasCustomerAddress)
        // {
        //     return RedirectToAction("PayCart");
        // }

        var vm = new HomeViewModel(_beService);
        return View(vm);
    }

    [HttpPost]
    public IActionResult EditAddress(EditAddressPostModel pm)
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

        return RedirectToAction("PayCart");
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
