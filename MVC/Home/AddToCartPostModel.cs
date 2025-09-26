using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class AddToCartPostModel
{
    public required Guid id { get; set; }
    public required int amount { get; set; }
}
