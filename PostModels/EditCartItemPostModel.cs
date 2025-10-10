using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class EditCartItemPostModel
{
    public required Guid id { get; set; }
    public required int amount { get; set; }
}
