namespace webbhelpuf.Data.Models;

public class Cart
{
    public Guid Id { get; set; }

    public required virtual Shop Shop { get; set; }
    public required virtual HashSet<CartItem> Items { get; set; }
}