using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class PayCartPostModel
{
    public Guid shopid { get; set; }
    public Guid customerid { get; set; }
    public Guid cartid { get; set; }
}    