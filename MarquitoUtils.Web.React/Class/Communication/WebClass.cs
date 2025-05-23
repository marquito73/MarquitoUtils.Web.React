﻿using MarquitoUtils.Main.Class.Entities.Sql;
using MarquitoUtils.Main.Class.Service.General;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Sql;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Communication.JSON;
using MarquitoUtils.Web.React.Class.Tools;
using MarquitoUtils.Web.React.Class.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
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
        /// View default location
        /// </summary>
        protected string ViewDefaultLocation { get; set; } = "";
        /// <summary>
        /// The translation service
        /// </summary>
        protected ITranslateService TranslateService { get; private set; }

        /// <summary>
        /// Web class
        /// </summary>
        /// <param name="webDataEngine">The web data engine</param>
        public WebClass(WebDataEngine webDataEngine)
        {
            this.WebDataEngine = webDataEngine;
            this.CurrentLanguage = webDataEngine.CurrentLanguage;

            string entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;

            this.ViewDefaultLocation = $"{entryAssemblyName}.Views";

            this.TranslateService = new TranslateService(webDataEngine.StartupOptions.Translations);
        }

        /// <summary>
        /// Get current language
        /// </summary>
        /// <returns>The current language</returns>
        protected LanguageType GetCurrentLanguage()
        {
            return LanguageUtils.GetLanguage(this.WebDataEngine.CurrentLanguage);
        }

        // TODO Determinate how to find implementations
        public T GetService<T>() where T : IEntityService
        {
            return default(T);
        }

        /// <summary>
        /// Get entity service
        /// </summary>
        /// <returns>The entity service</returns>
        public IEntityService GetEntityService()
        {
            return this.WebDataEngine.EntityService;
        }

        /// <summary>
        /// Get translation
        /// </summary>
        /// <typeparam name="T">The class need translation</typeparam>
        /// <param name="translateKey">The translate key</param>
        /// <returns>The translation</returns>
        protected string GetTranslation<T>(string translateKey) where T : class
        {
            LanguageType language = this.TranslateService.GetLanguageWithCultureInfo(this.CurrentLanguage); 
            return this.TranslateService.GetTranslation<T>(translateKey, language);
        }

        /// <summary>
        /// Get entity list
        /// </summary>
        /// <typeparam name="TEntityList">Entity list type</typeparam>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>List of entities</returns>
        protected TEntityList GetEntityList<TEntityList, TEntity>()
            where TEntityList : EntityList<TEntity>, new()
            where TEntity : Entity
        {
            TEntityList entityList = new TEntityList();
            entityList.EntityService = this.WebDataEngine.EntityService;

            return entityList;
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

        protected JsonResult GetSuccessJsonResult(string resultMessage = "", string resultTitle = "", object content = null)
        {
            JsonResultContent result = new JsonResultContent()
            {
                State = Enums.JSON.EnumJsonResponseState.Success.ToString().ToLower(),
                Title = resultTitle,
                Message = resultMessage,
                Data = content,
            };

            return this.GetJsonResult(result, 200);
        }

        protected JsonResult GetErrorJsonResult(string resultMessage = "", string resultTitle = "", object content = null, int statusCode = StatusCodes.Status400BadRequest)
        {
            JsonResultContent result = new JsonResultContent()
            {
                State = Enums.JSON.EnumJsonResponseState.Error.ToString().ToLower(),
                Title = resultTitle,
                Message = resultMessage,
                Data = content,
            };

            return this.GetJsonResult(result, statusCode);
        }

        /// <summary>
        /// Get JSON content result
        /// </summary>
        /// <param name="result">The content</param>
        /// <param name="statusCode">The HTTP satus code</param>
        /// <returns>JSON content result</returns>
        private JsonResult GetJsonResult(JsonResultContent result, int statusCode)
        {
            return new JsonResult(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
            })
            {
                StatusCode = statusCode,
            };
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

        /// <summary>
        /// Get redirect result
        /// </summary>
        /// <param name="redirectUrl">The url</param>
        /// <returns>Redirect result</returns>
        protected RedirectResult GetRedirectResult(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }

        /// <summary>
        /// Get redirect result
        /// </summary>
        /// <returns>Redirect result</returns>
        protected RedirectResult GetRedirectResult<TView>()
            where TView : WebView
        {
            string redirect = typeof(TView).FullName
                .Replace($"{this.ViewDefaultLocation}.", "")
                .Replace(".View", ".")
                .Replace(".", "/");

            return this.GetRedirectResult($"/{redirect}");
        }
    }
}
