using MarquitoUtils.Main.Files.Entities;
using MarquitoUtils.Main.Files.Services;
using Microsoft.AspNetCore.Http;

namespace MarquitoUtils.Web.React.Service
{
    public interface IWebFileService : IFileService
    {
        public CustomFile GetFileStreamFromWebFile(IFormFile formFile, string fileExtension);
    }
}
