namespace webbhelpuf.Data.Models;
public class ShopItemProperty
{
    public string Name { get; set; }
    public virtual HashSet<ShopItemPropertyOption> Options { get; set; }

    public bool Uploadable { get; set; }
    public virtual Image Image { get; set; }

}
