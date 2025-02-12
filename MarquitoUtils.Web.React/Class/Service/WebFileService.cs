using MarquitoUtils.Main.Class.Entities.File;
using MarquitoUtils.Main.Class.Service.Files;
using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Service
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
