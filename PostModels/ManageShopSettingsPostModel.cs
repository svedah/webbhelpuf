using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class ManageShopSettingsPostModel
{
    public Guid? id { get; set; }
    public string? title { get; set; }
    public string? swishnumber { get; set; }
    public string? description { get; set; }
    public string? layout { get; set; }
    public string? theme { get; set; }
    public int? baseshippingprice { get; set; }
    public IFormFile? logoimage { get; set; }
}
