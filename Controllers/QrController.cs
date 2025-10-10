// using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webbhelpuf.Services;
// using webbhelpuf.ViewModels;

using Net.Codecrete.QrCodeGenerator;

namespace webbhelpuf.Controllers;

public class QRController : Controller
{
    // private readonly ILogger<HomeController> _logger;
    // private readonly BeService _beService;

    public QRController(ILogger<HomeController> logger, BeService beService)
    {
        // _logger = logger;
        // _beService = beService;
    }

    // public ActionResult<byte[]> GeneratePng([FromQuery(Name = "text")] string text,
    //         [FromQuery(Name = "ecc")] int? ecc, [FromQuery(Name = "border")] int? borderWidth)
    [Route("/QR/PNG/{text}")]
    public ActionResult<byte[]> Png(string text = "")
    {
        string dtext = System.Web.HttpUtility.UrlDecode(text);

        // QrCode.Ecc[] errorCorrectionLevels = { QrCode.Ecc.Low, QrCode.Ecc.Medium, QrCode.Ecc.Quartile, QrCode.Ecc.High };
        // int ecc = 3; //Math.Clamp(ecc ?? 1, 0, 3);
        int borderWidth = 2; //Math.Clamp(borderWidth ?? 3, 0, 999999);

        // string text = "Test";

        var qrCode = QrCode.EncodeText(dtext, QrCode.Ecc.High/*errorCorrectionLevels[(int)ecc]*/);
        byte[] png = qrCode.ToPng(4, (int)borderWidth);
        return new FileContentResult(png, "image/png");
    }

}
