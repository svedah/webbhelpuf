using Microsoft.AspNetCore.Identity;
using webbhelpuf.Data;
using webbhelpuf.Data.Models;

public class IdentitySeeder
{
    private const string _admin = "admin";
    private const string _password = "Test123!";

    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<User> _userManager;

    public IdentitySeeder(ApplicationDbContext ctx, UserManager<User> userManager)
    {
        _ctx = ctx;
        _userManager = userManager;
    }
    public bool CreateAdminAccountIfEmpty()
    {
        if (!_ctx.Users.Any(u => u.UserName == _admin))
        {
            // var site = _ctx.Sites.FirstOrDefault();
            // var result = _userManager.CreateAsync(new User
            // {
            //     UserName = _admin,
            //     Email = "admin@bolindersbil.se",
            //     EmailConfirmed = true,
            //     SiteId = site.Id
                
            // }, _password).Result;
        }
        return true;
    }
}
