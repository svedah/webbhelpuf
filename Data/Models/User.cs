using Microsoft.AspNetCore.Identity;

namespace webbhelpuf.Data.Models;

public class User : IdentityUser
{
    public required virtual Shop Shop { get; set; }
}

