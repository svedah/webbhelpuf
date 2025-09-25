using System.ComponentModel.DataAnnotations;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class EmailHelper
{
    static public bool IsValidEmail(string input)
    {
        return new EmailAddressAttribute().IsValid(input);
    }
}