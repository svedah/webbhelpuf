
using Microsoft.AspNetCore.Identity;
using webbhelpuf.Data;
namespace webbhelpuf.Services;

public class BeService
{
    public readonly ApplicationDbContext DbContext;
    public readonly UserManager<IdentityUser> UserManager;
    public readonly RoleManager<IdentityRole> RoleManager;
    public readonly SignInManager<IdentityUser> SignInManager;
    public readonly IHttpContextAccessor HttpContextAccessor;
    public readonly IServiceProvider ServiceProvider;
    public readonly IWebHostEnvironment WebHostEnvironment;
    public readonly string wwwroot;

    public BeService
    (
        ApplicationDbContext dbContext,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment iWebHostEnvironment,
        IServiceProvider serviceProvider
    )
    {
        DbContext = dbContext;
        UserManager = userManager;
        SignInManager = signInManager;
        RoleManager = roleManager;
        HttpContextAccessor = httpContextAccessor;
        WebHostEnvironment = iWebHostEnvironment;
        ServiceProvider = serviceProvider;
        wwwroot = iWebHostEnvironment.WebRootPath + Path.DirectorySeparatorChar;
    }

    //var context = ServiceProvider.GetService<ApplicationDbContext>();

}
