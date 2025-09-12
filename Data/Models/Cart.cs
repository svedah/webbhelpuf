namespace webbhelpuf.Data.Models;

public class Cart
{
    public Guid Id { get; set; }

    public required Shop Shop { get; set; }
    public required HashSet<CartItem> Items { get; set; }
}