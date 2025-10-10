using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class ManageShopSocialMediasPostModel
{
    public Guid? id { get; set; }
    public string? facebook { get; set; }
    public string? instagram { get; set; }
    public string? linkedin { get; set; }
    public string? tiktok { get; set; }
    public string? youtube { get; set; }
}