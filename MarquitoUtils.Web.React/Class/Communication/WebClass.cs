using MarquitoUtils.Main.Class.Entities.Translation;
using MarquitoUtils.Main.Class.Service.General;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Sql;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Main.Class.Translations;
using MarquitoUtils.Web.React.Class.Communication.JSON;
using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection;
using static MarquitoUtils.Main.Class.Enums.EnumLang;

namespace MarquitoUtils.Web.React.Class.Communication
{
    /// <summary>
    /// Common class for views, ajax and actions
    /// </summary>
    public abstract class WebClass
    {
        /// <summary>
        /// The web data engine
        /// </summary>
        protected WebDataEngine WebDataEngine { get; private set; }
        /// <summary>
        /// Current language
        /// </summary>
        protected CultureInfo CurrentLanguage { get; private set; }
        /// <summary>
        /// Notify hub proxy, for send and get data with signalR
        /// </summary>
        protected NotifyHubProxy NotifyHubProxy { get; set; }

        /// <summary>
        /// Web class
        /// </summary>
        /// <param name="webDataEngine">The web data engine</param>
        public WebClass(WebDataEngine webDataEngine)
        {
            this.WebDataEngine = webDataEngine;
            this.CurrentLanguage = webDataEngine.CurrentLanguage;
        }

        public WebClass(WebDataEngine webDataEngine, NotifyHubProxy notifyHubProxy) : this(webDataEngine)
        {
            this.NotifyHubProxy = notifyHubProxy;
        }

        protected enumLang GetCurrentLanguage()
        {
            return LanguageUtils.GetLanguage(this.WebDataEngine.CurrentLanguage);
        }

        // TODO Determinate how to find implementations
        public T GetService<T>() where T : IEntityService
        {
            return default(T);
        }

        /// <summary>
        /// Get the database context
        /// </summary>
        /// <typeparam name="T">Type of database context</typeparam>
        /// <returns>The database context</returns>
        public T GetDbContext<T>() where T : DefaultDbContext
        {
            T result = default(T);

            if (Utils.IsNotNull(this.WebDataEngine.DbContext) && this.WebDataEngine.DbContext is T)
            {
                result = (T)this.WebDataEngine.DbContext;
            }

            return result;
        }

        /// <summary>
        /// Get entity service
        /// </summary>
        /// <returns>The entity service</returns>
        /// <exception cref="Exception"></exception>
        public IEntityService GetEntityService()
        {
            IEntityService entityService = new EntityService();

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
        /// Get translation
        /// </summary>
        /// <typeparam name="T">The class need translation</typeparam>
        /// <param name="translateKey">The translate key</param>
        /// <returns>The translation</returns>
        protected string GetTranslation<T>(string translateKey) where T : class
        {
            enumLang lang = Translate.GetLanguageWithCultureInfo(this.CurrentLanguage); 
            return Translate.GetTranslation<T>(translateKey, lang);
        }

        /// <summary>
        /// Get content result
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns>Content result</returns>
        protected ContentResult GetContentResult(string content)
        {
            ContentResult result = new ContentResult();

            result.Content = content;

            return result;
        }

        protected JsonResult GetSuccessJsonResult(string resultMessage = "", object content = null)
        {
            JsonResultContent result = new JsonResultContent()
            {
                State = Enums.JSON.EnumJsonResponseState.Success.ToString().ToLower(),
                Message = resultMessage,
                Data = content,
            };

            return this.GetJsonResult(result);
        }

        protected JsonResult GetErrorJsonResult(string resultMessage = "", object content = null)
        {
            JsonResultContent result = new JsonResultContent()
            {
                State = Enums.JSON.EnumJsonResponseState.Error.ToString().ToLower(),
                Message = resultMessage,
                Data = content,
            };

            return this.GetJsonResult(result);
        }

        /// <summary>
        /// Get JSON content result
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns>JSON content result</returns>
        private JsonResult GetJsonResult(JsonResultContent result)
        {
            return new JsonResult(result);
        }

        /// <summary>
        /// Get file content result
        /// </summary>
        /// <param name="fileBytes">File bytes</param>
        /// <param name="fileName">File name</param>
        /// <returns>File content result</returns>
        protected FileContentResult GetFileResult(byte[] fileBytes, string fileName)
        {
            FileContentResult result = new FileContentResult(fileBytes, "application/octet-stream");

            result.FileDownloadName = fileName;

            return result;
        }
    }
}
