using System.Text;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class SessionHelper
{
    //returns guid empty on no cartsessionkey set or malformed guid
    public static Guid GetCartId(BeService _beService)
    {
        Guid output = Guid.Empty;

        var session = _beService.HttpContextAccessor.HttpContext?.Session;
        if (session is not null)
        {
            var sessionString = session.GetString(Constants.CARTSESSIONKEY);
            if (sessionString is not null)
            {
                Guid.TryParse(sessionString, out output);
            }
        }

        return output;
    }

    public static bool HasCartId(BeService _beService)
    {
        return !GetCartId(_beService).Equals(Guid.Empty);
    }

    public static void SetCartId(BeService _beService, Guid input)
    {
        var session = _beService.HttpContextAccessor.HttpContext?.Session;
        if (session is not null && input != Guid.Empty)
        {
            session.SetString(Constants.CARTSESSIONKEY, input.ToString());
        }
    }


    // public static string GetCartString(BeService _beService)
    // {
    //     string output = string.Empty;
    //     var session = _beService.HttpContextAccessor.HttpContext?.Session;

    //     if (session is not null)
    //     {
    //         output = session.GetString(Constants.CARTSESSIONKEY) ?? string.Empty;
    //     }

    //     return output;
    // }

    // public static void SetCartString(BeService _beService, string input)
    // {
    //     var session = _beService.HttpContextAccessor.HttpContext?.Session;
    //     if (session is not null && input is not null)
    //     {
    //         session.SetString(Constants.CARTSESSIONKEY, input);
    //     }
    // }

    // public static void DeleteCartString(BeService _beService)
    // {
    //     var session = _beService.HttpContextAccessor.HttpContext?.Session;

    //     if (session is not null)
    //     {
    //         session.Remove(Constants.CARTSESSIONKEY);
    //     }
    // }
}