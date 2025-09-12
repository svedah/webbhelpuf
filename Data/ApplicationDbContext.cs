using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using webbhelpuf.Data.Models;

namespace webbhelpuf.Data;

public class ApplicationDbContext : IdentityDbContext
{
    // public DbSet<Type> Table { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
