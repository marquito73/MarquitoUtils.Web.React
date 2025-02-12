using MarquitoUtils.Main.Class.Entities.File;
using MarquitoUtils.Main.Class.Service.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Service
{
    public interface IWebFileService : IFileService
    {
        public CustomFile GetFileStreamFromWebFile(IFormFile formFile, string fileExtension);
    }
}
