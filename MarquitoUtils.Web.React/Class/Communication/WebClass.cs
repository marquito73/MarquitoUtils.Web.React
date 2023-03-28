using MarquitoUtils.Main.Class.Entities.Translation;
using MarquitoUtils.Main.Class.Service.General;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Communication
{
    public abstract class WebClass
    {
        public WebDataEngine WebDataEngine { get; private set; }

        private ITranslateService TranslateService { get; set; }

        public WebClass(WebDataEngine webDataEngine)
        {
            this.WebDataEngine = webDataEngine;
            this.InitTranslations(Assembly.GetEntryAssembly());
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

        /// <summary>
        /// Init translations from an XML translation file
        /// </summary>
        /// <param name="translationFilePath">The path to access translations</param>
        private void InitTranslations(Assembly translationFilePath)
        {
            List<Translation> translations = this.WebDataEngine
                .GetSessionValue<List<Translation>>("MainTranslations");

            this.TranslateService = new TranslateService(translations);

            if (Utils.IsEmpty(translations))
            {
                translations = this.TranslateService.GetTranslations(
                    @Properties.Resources.translateFilePath, translationFilePath);

                this.WebDataEngine.SetSessionValue("MainTranslations", translations);

                this.TranslateService = new TranslateService(translations);
            }
        }

        protected string GetTranslation<T>(string translateKey) where T : class
        {
            return this.TranslateService.GetTranslation<T>(translateKey);
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
