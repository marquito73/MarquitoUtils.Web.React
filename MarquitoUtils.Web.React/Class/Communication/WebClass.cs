using MarquitoUtils.Main.Class.Service.General;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Communication
{
    public abstract class WebClass
    {
        public WebDataEngine WebDataEngine { get; private set; }

        public WebClass(WebDataEngine webDataEngine)
        {
            this.WebDataEngine = webDataEngine;
        }
        // TODO Déterminer les implémentations
        public T GetService<T>() where T : EntityService
        {
            return default(T);
        }

        public T GetDbContext<T>() where T : DbContext
        {
            T result = default(T);

            if (Utils.IsNotNull(this.WebDataEngine.DbContext) && this.WebDataEngine.DbContext is T)
            {
                result = (T)this.WebDataEngine.DbContext;
            }

            return result;
        }

        public EntityService GetEntityService()
        {
            EntityService entityService = new EntityServiceImpl();

            if (Utils.IsNotNull(this.WebDataEngine.DbContext))
            {
                entityService.DbContext = this.WebDataEngine.DbContext;
            }
            else
            {
                throw new Exception("Can't get entity service without DbContext specified to the WebClass");
            }

            return entityService;
        }

        protected ContentResult GetContentResult(string content)
        {
            ContentResult result = new ContentResult();

            result.Content = content;

            return result;
        }

        protected JsonResult GetJsonResult(object content)
        {
            return new JsonResult(content);
        }

        protected FileContentResult GetFileResult(byte[] fileBytes, string fileName)
        {
            FileContentResult result = new FileContentResult(fileBytes, "application/octet-stream");

            result.FileDownloadName = fileName;

            return result;
        }
    }
}
