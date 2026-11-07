using MarquitoUtils.Main.Common.Tools;
using MarquitoUtils.Main.Files.Entities;
using MarquitoUtils.Main.Files.Services;
using Microsoft.AspNetCore.Http;

namespace MarquitoUtils.Web.React.Service
{
    public class WebFileService : FileService, IWebFileService
    {
        public CustomFile GetFileStreamFromWebFile(IFormFile formFile, string fileExtension)
        {
            CustomFile file;

            if (this.TextExtensions.Contains(fileExtension))
            {
                // We can read file as text
                using (StreamReader reader = new StreamReader(formFile.OpenReadStream()))
                {
                    file = new CustomFile(formFile.Name, fileExtension, reader.ReadToEnd());
                }
            }
            else
            {
                file = new CustomFile(formFile.Name, fileExtension, Utils.ReadAllBytes(formFile.OpenReadStream()));
            }

            return file;
        }
    }
}
