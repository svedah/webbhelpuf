using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using webbhelpuf.Data.Models;

namespace webbhelpuf.Data;

public class ApplicationDbContext : IdentityDbContext
{
    // public DbSet<Type> Table { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerInfo> CustomerInfos { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<ShopContactInfo> ShopContactInfos { get; set; }
    public DbSet<ShopItem> ShopItems { get; set; }

    public DbSet<ShopItemProperty> ShopItemProperties { get; set; }
    public DbSet<ShopItemPropertyOption> ShopItemPropertyOptions { get; set; }
    public DbSet<ShopSetting> ShopSettings { get; set; }
    public DbSet<ShopSocialMedia> ShopSocialMedias { get; set; }
    public DbSet<User> ShopOwners { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        ;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite()
        .UseSeeding((context, _) =>
        {
            // var um = new 
            // var testBlog = context.Set<Blog>().FirstOrDefault(b => b.Url == "http://test.com");
            // if (testBlog == null)
            // {
            //     context.Set<Blog>().Add(new Blog { Url = "http://test.com" });
            //     context.SaveChanges();
            // }
        });
        // .UseAsyncSeeding(async (context, _, cancellationToken) =>
        // {
        //     ;
        //     // var testBlog = await context.Set<Blog>().FirstOrDefaultAsync(b => b.Url == "http://test.com", cancellationToken);
        //     // if (testBlog == null)
        //     // {
        //     //     context.Set<Blog>().Add(new Blog { Url = "http://test.com" });
        //     //     await context.SaveChangesAsync(cancellationToken);
        //     // }
        // });
}
