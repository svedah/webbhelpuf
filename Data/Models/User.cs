using Microsoft.AspNetCore.Identity;

namespace webbhelpuf.Data.Models;

public class User : IdentityUser
{
    public required Shop Shop { get; set; }
}