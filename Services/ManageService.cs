using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data.Models;
using webbhelpuf.Factories;
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
            var settings = _beService.DbContext.ShopSettings
                .Where(e => e.Id == input.id)
                .Include(e => e.LogoImage)
                .Include(e => e.ContactInfo)
                .First();
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
                        var ms = new MemoryStream();
                        input.logoimage.OpenReadStream().CopyTo(ms);//copy - logoimage stream is writable
                        var rawdata = ms.ToArray();

                        Image newLogoImage = DataModelFactory.CreateDummyImage();
                        if (UpdateSettings_Image(rawdata, ref newLogoImage))
                        {
                            //store
                            _beService.DbContext.Images.Add(newLogoImage);
                            var oldImage = settings.LogoImage;
                            settings.LogoImage = newLogoImage;
                            //TODO: delete old picture and db entry for image
                            _beService.DbContext.Images.Remove(oldImage);
                            var storePath = _beService.wwwroot + Path.DirectorySeparatorChar + "img" + Path.DirectorySeparatorChar + "logo" + Path.DirectorySeparatorChar;
                            if (File.Exists(storePath + oldImage.Filename))
                            {
                                File.Delete(storePath + oldImage.Filename);
                            }
                        }
                    }
                    _beService.DbContext.SaveChanges();
                }


            }
        }
    }

    // private bool UpdateSettings_Image(MemoryStream ms, ShopSetting settings)
    private bool UpdateSettings_Image(byte[] arr, ref Image dbImage)
    {
        bool output = false;
        SixLabors.ImageSharp.Image image;
        // if (ImageSharpHelper.OpenImageFromStream(ms, out image))
        if (ImageSharpHelper.OpenImageFromArray(arr, out image))
        {
            //crop
            SixLabors.ImageSharp.Image croppedImage = ImageSharpHelper.CropToSquare(image);

            // resize image
            SixLabors.ImageSharp.Image resizedImage = ImageSharpHelper.Resize(croppedImage, Constants.LOGOIMAGEWIDTH, Constants.LOGOIMAGEHEIGHT);

            //create db image
            var newImageId = Guid.NewGuid();
            var newImage = new Image
            {
                Id = newImageId,
                Filename = newImageId.ToString() + ".jpeg",
                AltText = "Logo"
            };

            //store in wwwroot/img/logo/
            var newImageData = ImageSharpHelper.SaveImageToStream(resizedImage).ToArray();
            var storePath = _beService.wwwroot + Path.DirectorySeparatorChar + "img" + Path.DirectorySeparatorChar + "logo" + Path.DirectorySeparatorChar;
            File.WriteAllBytes(storePath + newImage.Filename, newImageData);

            dbImage = newImage;

            //erase former image from wwwroot/img and db on success
            // var currentImage = settings.LogoImage;
            // if (currentImage is not null)
            // {
            //     var storePath = _beService.wwwroot + Path.DirectorySeparatorChar + "img" + Path.DirectorySeparatorChar + "logo" + Path.DirectorySeparatorChar;
            //     if (File.Exists(storePath + currentImage.Filename))
            //     {
            //         File.Delete(storePath + currentImage.Filename);
            //     }
            //     _beService.DbContext.Images.Remove(currentImage);
            // }

            //store new image in db
            // _beService.DbContext.Images.Add(newImage);
            // _beService.DbContext.SaveChanges();
            // settings.LogoImage = _beService.DbContext.Images.Where(e => e.Id == newImageId).First();
            // _beService.DbContext.SaveChanges();

            output = true;
        }
        return output;
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
