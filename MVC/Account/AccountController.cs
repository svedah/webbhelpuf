using Microsoft.AspNetCore.Mvc;
using webbhelpuf.Services;
using webbhelpuf.ViewModels;

namespace webbhelpuf.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BeService _beService;

    public AccountController(ILogger<HomeController> logger, BeService beService)
    {
        _logger = logger;
        _beService = beService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel lvm)
    {
        IActionResult output = RedirectToAction("Index", "Account");

        var user = await _beService.UserManager.FindByEmailAsync(lvm.UserName);
        if (user is not null)
        {
            await _beService.SignInManager.SignOutAsync();
            if ((await _beService.SignInManager.PasswordSignInAsync(user, lvm.Password, false, false)).Succeeded)
            {
                //TODO: Redirect to /Manage/Index
                output = RedirectToAction("Index", "Home");
            }
        }

        return output;
    }

    public IActionResult Logout()
    {
        _beService.SignInManager.SignOutAsync().Wait();
        return RedirectToAction("Index", "Home");
    }
}