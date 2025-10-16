using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualBasic;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;

namespace webbhelpuf.Data.Seed;



public class Seeder
{
    private BeService _srv;
    public Seeder(BeService beService)
    {
        _srv = beService;
    }

    public static void SeedOnEmpty(BeService _beService)
    {
        if (Helpers.DomainHelper.ExtractSubDomain(_beService).Equals("localhost"))
        {
            new webbhelpuf.Data.Seed.Seeder(_beService).Seed();
            new webbhelpuf.Data.Seed.Seeder(_beService).ClearCartsAndCustomers();
        }
    }
    //TODO: Seed admin OK

    //TODO: Seed our shop OK

    //TODO: Seed our items

    //TODO: Seed example shop

    public void Seed()
    {
        var ctx = _srv.DbContext;
        if (!ctx.Users.Any())
        {
            //TODO: programmatically "dotnet ef database update" here ???
            SeedIdentityRoles().Wait();
            SeedAdministrator().Wait();//TODO

            var shopSocialMedia = SeedShopSocialMedia();
            var shopContactInfo = SeedShopContactInfo(shopSocialMedia);
            var logo = SeedShopLogo();
            var shopSetting = SeedShopSetting(shopContactInfo, logo);
            var shop = SeedShop(shopSetting);
            var shopitem1 = SeedShopItem1(shop);
            var shopitem2 = SeedShopItem2(shop);
            var shopitems = new List<ShopItem> { shopitem1, shopitem2 };
            SeedItemsToShop(shop, shopitems);

            var user = SeedUser(shop);//TODO
            ctx.SaveChanges();
        }

        // if (!ctx.ShopSettings.Any())
        // {
        //     // var ss = new ShopSetting()
        //     // {
        //     //     BaseShippingPrice = 123,
        //     //     Description = "Beskrivningen",
        //     //     Title = "Titeln",
        //     //     HostName = "www",
        //     //     Theme = "Theme 1"
        //     // };
        //     // beService.DbContext.ShopSettings.Add(ss);
        //     // beService.DbContext.SaveChanges();
        //     // SeedUser(context);//https://stackoverflow.com/questions/34343599/how-to-seed-users-and-roles-with-code-first-migration-using-identity-asp-net-cor
        // }
    }

    private async Task SeedAdministrator()
    {
        var user = new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            // Shop = null,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            UserName = "lara@webbhelp.se",
            Email = "lara@" + webbhelpuf.Shared.Constants.DOMAINNAME,
            NormalizedEmail = "LARA@" + webbhelpuf.Shared.Constants.DOMAINNAME.ToUpper(),
            NormalizedUserName = "LARA@" + webbhelpuf.Shared.Constants.DOMAINNAME.ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
        };

        if (!_srv.DbContext.Users.Any(x => x.UserName == user.UserName))
        {
            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "lara1234");

            var userStore = new UserStore<IdentityUser>(_srv.DbContext);
            userStore.CreateAsync(user).Wait();

            //assign role
            var gotUser = _srv.UserManager.FindByEmailAsync(user.Email).Result;
            if (gotUser is not null)
            {
                _srv.UserManager.AddToRoleAsync(gotUser, "Administrator").Wait();
            }
        }

        await _srv.DbContext.SaveChangesAsync();
    }

    private Shop SeedShop(ShopSetting shopSetting)
    {
        var items = new HashSet<ShopItem>();
        var orders = new HashSet<Order>();

        return new Shop
        {
            Id = Guid.NewGuid(),
            Prefix = "www",
            Settings = shopSetting,
            Items = items,
            Orders = orders
        };

    }

    private ShopSetting SeedShopSetting(ShopContactInfo sci, Image logo)
    {
        return new ShopSetting
        {
            Id = Guid.NewGuid(),
            BaseShippingPrice = 0,
            Description = "WebbHelp UF - hjälper ditt UF-företag att starta en webbshop",
            ContactInfo = sci,
            SwishNumber = "+461234567890",
            Title = "WebbHelp",
            Layout = "Standard",
            Theme = "Standard",
            LogoImage = logo
        };
    }

    private ShopContactInfo SeedShopContactInfo(ShopSocialMedia ssm)
    {
        return new ShopContactInfo
        {
            Email = "em@il.com",
            MobileNumber = string.Empty,
            SocialMedias = ssm
        };
    }

    private void SeedItemsToShop(Shop shop, List<ShopItem> shopitems)
    {
        foreach (ShopItem item in shopitems)
        {
            shop.Items.Add(item);
        }
    }

    private ShopItem SeedShopItem1(Shop shop)
    {
        return new ShopItem
        {
            Title = "Item 1",
            Description = "Beskrivning Item 1",
            Shop = shop,
            Price = 200,
            ItemsAvailable = 1000,
            Order = 1,
            PrimaryImage = new Image
            {
                Id = Guid.NewGuid(),
                AltText = "Bild Item 1",
                Filename = "00000000-0000-0000-0000-000000000000.jpeg"
            },
            Images = new HashSet<Image>()
        };
    }

    private ShopItem SeedShopItem2(Shop shop)
    {
        return new ShopItem
        {
            Title = "Item 2",
            Description = "Beskrivning Item 2",
            Shop = shop,
            Price = 300,
            ItemsAvailable = 1000,
            Order = 2,
            PrimaryImage = new Image
            {
                Id = Guid.NewGuid(),
                AltText = "Bild Item 2",
                Filename = "00000000-0000-0000-0000-000000000000.jpeg"
            },
            Images = new HashSet<Image>()
        };
    }

    private Image SeedShopLogo()
    {
        return new Image
        {
            Id = Guid.NewGuid(),
            AltText = "WebbHelp Logo",
            Filename = "WebbHelp_Logo.jpeg"
        };
    }

    private ShopSocialMedia SeedShopSocialMedia()
    {
        return new ShopSocialMedia
        {
            Id = Guid.NewGuid(),
            Facebook = "https://www.facebook.com/",
            Instagram = "https://www.instagram.com/",
            LinkedIn = "https://www.linkedin.com/",
            TikTok = "https://www.tiktok.com/",
            YouTube = "https://www.youtube.com/"
        };
    }

    private async Task<User> SeedUser(Shop shop)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Shop = shop,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            UserName = "isac@webbhelp.se",
            Email = "isac@" + webbhelpuf.Shared.Constants.DOMAINNAME,
            NormalizedEmail = "ISAC@" + webbhelpuf.Shared.Constants.DOMAINNAME.ToUpper(),
            NormalizedUserName = "ISAC@" + webbhelpuf.Shared.Constants.DOMAINNAME.ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
        };

        if (!_srv.DbContext.Users.Any(x => x.UserName == user.UserName))
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "isac1234");

            var userStore = new UserStore<IdentityUser>(_srv.DbContext);
            userStore.CreateAsync(user).Wait();

            //assign role
            var gotUser = _srv.UserManager.FindByEmailAsync(user.Email).Result;
            if (gotUser is not null)
            {
                await _srv.UserManager.AddToRoleAsync(gotUser, "Owner");
            }
        }

        await _srv.DbContext.SaveChangesAsync();
        return user;
    }

    private async Task SeedIdentityRoles()
    {
        var roles = new string[]{
            "Administrator",
            "Owner"
        };

        foreach (string role in roles)
        {
            await _srv.RoleManager.CreateAsync(new IdentityRole(role));
            // var roleStore = new RoleStore<IdentityRole>(_srv.DbContext);
            // roleStore.AutoSaveChanges = true;
            // if (!_srv.DbContext.Roles.Any(x => x.Name == role))
            // {
            //     roleStore.CreateAsync(new IdentityRole(role)).Wait();
            // }
        }
        await _srv.DbContext.SaveChangesAsync();
    }

    private void ClearCartsAndCustomers()
    {
        for (int i = 0; i < 4; i++)
        {
            _srv.DbContext.CartItems.RemoveRange(_srv.DbContext.CartItems);
            _srv.DbContext.CustomerInfos.RemoveRange(_srv.DbContext.CustomerInfos);
            _srv.DbContext.Customers.RemoveRange(_srv.DbContext.Customers);
            _srv.DbContext.Carts.RemoveRange(_srv.DbContext.Carts);
        }
    }
}