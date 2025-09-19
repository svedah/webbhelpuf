using Microsoft.AspNetCore.Connections;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class UploadHelper
{
        public static Guid UploadImage(IFormFile file, string path)
        {
            Guid id = Guid.NewGuid();

            string fileSuffix = "jpg"; //FileTypeHelper.FileTypeToSuffix(file.ContentType);
            string pathFileNameImage = path + id.ToString() + "." + fileSuffix;
            // string pathFileNameCard = path + "card_" + id.ToString() + ".jpg";

            MemoryStream ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms);

        // var cardImage = ImageHelper.CreateSquareImage(ms, Constants.CardImageSize, Constants.CardImageSize);
        WriteStreamToFile(ms, pathFileNameImage);//.Wait();
            // WriteStreamToFile(cardImage, pathFileNameCard).Wait();

            return id;
        }

        private static /*async Task*/ void WriteStreamToFile(Stream input, string pathFileName)
        {
            input.Seek(0, SeekOrigin.Begin);
            using (var fileStream = new FileStream(pathFileName, FileMode.Create, FileAccess.Write))
            {
                // await input.CopyToAsync(fileStream);
                input.CopyTo(fileStream);
            }
        }

}