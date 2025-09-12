
using Microsoft.AspNetCore.Identity;
using webbhelpuf.Data;
namespace webbhelpuf.Services;

public class BeService
{
    public readonly ApplicationDbContext DbContext;
    public readonly UserManager<IdentityUser> UserManager;
    public readonly IHttpContextAccessor HttpContextAccessor;
    public readonly IWebHostEnvironment WebHostEnvironment;


    public BeService
    (
        ApplicationDbContext dbContext,
        UserManager<IdentityUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment iWebHostEnvironment
    )
    {
        DbContext = dbContext;
        UserManager = userManager;
        HttpContextAccessor = httpContextAccessor;
        WebHostEnvironment = iWebHostEnvironment;
    }

}
