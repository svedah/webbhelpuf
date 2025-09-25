using webbhelpuf.Helpers;
using webbhelpuf.PostModels;
using webbhelpuf.Shared;
namespace webbhelpuf.Services;

public class ManageService
{
    private readonly BeService _beService;

    public ManageService(BeService beService)
    {
        _beService = beService;
    }


    public void UpdateContactInfo(ManageShopContactInfoPostModel input)
    {
        if (input.id is not null)
        {
            var contactinfo = _beService.DbContext.ShopContactInfos.Where(e => e.Id == input.id).First();
            if (contactinfo is not null)
            {
                var shopsettings = _beService.DbContext.ShopSettings.Where(e => e.ContactInfo == contactinfo).First();
                if (shopsettings is not null)
                {
                    var shop = _beService.DbContext.Shops.Where(e => e.Settings == shopsettings).First();
                    var subdomain = Helpers.DomainHelper.ExtractSubDomain(_beService);
                    bool shopownscontactinfo = shop.Prefix.ToLower().Equals(subdomain.ToLower());
                    if (shopownscontactinfo)
                    {
                        //email
                        if (input.email is not null && input.email.Length >= 6 && EmailHelper.IsValidEmail(input.email))
                        {
                            contactinfo.Email = input.email;
                        }

                        //mobile number
                        if (input.mobilenumber is not null && input.mobilenumber.Length >= 10)
                        {
                            contactinfo.MobileNumber = input.mobilenumber;
                        }
                        _beService.DbContext.SaveChanges();
                    }
                }
            }
        }
    }


    public void UpdateSettings(ManageShopSettingsPostModel input)
    {
        if (input.id is not null)
        {
            var settings = _beService.DbContext.ShopSettings.Where(e => e.Id == input.id).First();
            if (settings is not null)
            {
                var shop = _beService.DbContext.Shops.Where(e => e.Settings == settings).First();
                var subdomain = Helpers.DomainHelper.ExtractSubDomain(_beService);
                bool shopownssettings = shop.Prefix.ToLower().Equals(subdomain.ToLower());
                if (shopownssettings)
                {
                    //title
                    if (input.title is not null)
                    {
                        settings.Title = input.title;
                    }
                    //description
                    if (input.description is not null)
                    {
                        settings.Description = input.description;
                    }
                    //layout
                    if (input.layout is not null && Constants.AVAILABLE_LAYOUTS.Contains(input.layout))
                    {
                        settings.Layout = input.layout;
                    }
                    //theme
                    if (input.theme is not null && Constants.AVAILABLE_THEMES.Contains(input.theme))
                    {
                        settings.Theme = input.theme;
                    }
                    //swish
                    if (input.swishnumber is not null && input.swishnumber.Length > 0)
                    {
                        settings.SwishNumber = input.swishnumber;
                    }
                    //baseshippingprice
                    if (input.baseshippingprice is not null && input.baseshippingprice >= 0 && input.baseshippingprice <= 1000)
                    {
                        settings.BaseShippingPrice = (int)input.baseshippingprice;
                    }
                    //TODO: imagelogo
                    if (input.logoimage is not null)
                    {
                        throw new Exception("NOT DONE HERE!");
                    }
                    _beService.DbContext.SaveChanges();
                }


            }
        }
    }

    public void UpdateSocialMedia(ManageShopSocialMediasPostModel input)
    {
        if (input.id is not null)
        {
            var socialmedia = _beService.DbContext.ShopSocialMedias.Where(e => e.Id == input.id).First();
            if (socialmedia is not null)
            {
                var contactinfo = _beService.DbContext.ShopContactInfos.Where(e => e.SocialMedias == socialmedia).First();
                if (contactinfo is not null)
                {
                    var shopsettings = _beService.DbContext.ShopSettings.Where(e => e.ContactInfo == contactinfo).First();
                    if (shopsettings is not null)
                    {
                        var shop = _beService.DbContext.Shops.Where(e => e.Settings == shopsettings).First();
                        var subdomain = Helpers.DomainHelper.ExtractSubDomain(_beService);
                        bool shopownssocialmedia = shop.Prefix.ToLower().Equals(subdomain.ToLower());
                        if (shopownssocialmedia)
                        {
                            if (input.facebook is not null && UriHelper.IsValidUri(input.facebook))
                            {
                                socialmedia.Facebook = input.facebook;
                            }
                            if (input.instagram is not null && UriHelper.IsValidUri(input.instagram))
                            {
                                socialmedia.Instagram = input.instagram;
                            }
                            if (input.linkedin is not null && UriHelper.IsValidUri(input.linkedin))
                            {
                                socialmedia.LinkedIn = input.linkedin;
                            }
                            if (input.tiktok is not null && UriHelper.IsValidUri(input.tiktok))
                            {
                                socialmedia.TikTok = input.tiktok;
                            }
                            if (input.youtube is not null && UriHelper.IsValidUri(input.youtube))
                            {
                                socialmedia.YouTube = input.youtube;
                            }
                        }
                    }
                }
            }


        }
    }

}
