namespace webbhelpuf.Data.Models;

public class ShopContactInfo
{
    public Guid Id { get; set; }

    public required string Email { get; set; }
    public required string MobileNumber { get; set; }
    public required ShopSocialMedia SocialMedias { get; set; }

}