using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class UriHelper
{
    static public bool IsValidUri(string input)
    {
        Uri uriResult;
        bool result = Uri.TryCreate(input, UriKind.Absolute, out uriResult);
        bool prot = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        return result && prot;
    }
}