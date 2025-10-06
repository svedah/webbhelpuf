using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data.Models;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class ShopHelper
{
    /// <summary>
    /// Returnerar användares nuvarande shop eller "www"-shoppen om nuvarande shop inte finns.
    /// </summary>
    public static Shop GetCurrentShop(BeService _beService)
    {
        var domain = DomainHelper.ExtractSubDomain(_beService);
        Shop output = _beService.DbContext.Shops
                        .Where(e => e.Prefix == domain)
                        .Include(e => e.Settings)
                        .Include(e => e.Settings.ContactInfo)
                        .Include(e => e.Settings.ContactInfo.SocialMedias)
                        .Include(e => e.Settings.LogoImage)
                        .Include(e => e.Items)
                        .Include(e => e.Orders)
                        .First();
        // return _beService.DbContext.Shops.Where(e => e.Prefix == domain).First();
        return output;
    }
}