using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class EditCartPostModel
{
    public required Guid id { get; set; }
    public required int amount { get; set; }
}
