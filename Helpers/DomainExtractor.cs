using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class DomainHelper
{

    ///<summary>
    /// Extraherar och returnerar subdomän
    /// Exempel: www.example.com -> www 
    ///</summary>
    static public string ExtractHost(BeService beService)
    {
        string domain = string.Empty;

        if (beService.HttpContextAccessor.HttpContext is not null)
        {
            domain = beService.HttpContextAccessor.HttpContext.Request.Host.Host;
        }

        string output = domain.Replace("." + Constants.DOMAINNAME, string.Empty)
                              .Replace(Constants.DOMAINNAME, string.Empty);

        if (output.Length == 0)
        {
            output = Constants.DEFAULTDOMAIN;
        }

        return output;
    }

    static public Guid GetSubDomainId(BeService beService)
    {
        string host = ExtractHost(beService);
        //TODO: fetch id from db
        return Guid.Empty;
    }
}