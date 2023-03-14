using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Main.Class.Tools.Logger;
using MarquitoUtils.Web.React.Class.Attributes;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Components.General;
using MarquitoUtils.Web.React.Class.Tools;
using MarquitoUtils.Web.React.Class.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Controllers
{
    [Route("home")]
    public abstract class DefaultController : Controller
    {
        protected ILogger<DefaultController> _logger;
        protected string AjaxDefaultLocation { get; set; } = "";
        protected string MainAjaxLocationPath { get; set; } = "MarquitoUtils.Web.React.Class.Ajax";
        protected Assembly MainAssembly { get; set; } = Assembly.GetExecutingAssembly();
        protected string ActionDefaultLocation { get; set; } = "";
        protected string ViewDefaultLocation { get; set; } = "";
        protected string MainReactFile { get; set; } = "/dist/main.js";
        protected DbContext DbContext { get; set; }

        protected DefaultController(ILogger<DefaultController> logger)
        {
            this._logger = logger;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public virtual IActionResult Index()
        {
            return View();
        }

        [Route("ajax")]
        [RequestSizeLimit(1_000_000_000)]
        [AllowCrossSiteJson]
        public virtual IActionResult ExecAjax()
        {
            return this.ExecAjax(Assembly.GetExecutingAssembly());
        }

        protected IActionResult ExecAjax(Assembly assembly)
        {
            IActionResult ajaxResult = new ContentResult();

            // Parameters in query
            List<Parameter> parameters = AjaxHelper.GetValuesFromUrl(this.Request.QueryString.Value);
            // Init web data engine
            WebDataEngine webDataEngine = new WebDataEngine(this.HttpContext, this.DbContext);
            webDataEngine.AjaxParameters = parameters;
            // Get files if we have it
            if (this.Request.HasFormContentType && Utils.IsNotNull(this.Request.Form))
            {
                webDataEngine.Files = this.Request.Form.Files.ToList();
            }
            // Ajax name
            string ajax_name = webDataEngine.GetStringFromQuery("ajax_name");
            // Ajax action
            string ajax_action = webDataEngine.GetStringFromQuery("ajax_action");

            Type ajax = assembly.GetType(AjaxHelper.GetAjaxLocation(this.AjaxDefaultLocation, ajax_name));

            if (Utils.IsNull(ajax))
            {
                ajax = this.MainAssembly
                    .GetType(AjaxHelper.GetAjaxLocation(this.MainAjaxLocationPath, ajax_name));
            }

            if (Utils.IsNotNull(ajax))
            {
                // Create ajax instance
                WebAjax webAjax = (WebAjax)Activator.CreateInstance(ajax, webDataEngine);
                // Execute the ajax
                ajaxResult = webAjax.Exec(ajax_action);
            }
            else
            {
                ajaxResult = Content(ajax_name + " not found !");
            }

            return ajaxResult;
        }

        [Route("action")]
        [AllowCrossSiteJson]
        public virtual IActionResult ExecAction(string action_name, string action_action)
        {
            return this.ExecAction(action_name, action_action, Assembly.GetExecutingAssembly());
        }

        protected IActionResult ExecAction(string action_name, string action_action, Assembly assembly)
        {
            string actionResult = "";

            List<Parameter> parameters = ActionHelper.GetValuesFromUrl(this.Request.QueryString.Value);

            // Init web data engine
            WebDataEngine webDataEngine = new WebDataEngine(this.HttpContext, this.DbContext);
            webDataEngine.AjaxParameters = parameters;
            // If action_name or action not found, try found actionName and action inside parameters
            if (Utils.IsEmpty(action_name) || Utils.IsEmpty(action_action))
            {
                action_name = Utils.GetAsString(parameters.Where(param => param.ParameterName.Equals("actionName"))
                    .First().ParameterValue);
                action_action = Utils.GetAsString(parameters.Where(param => param.ParameterName.Equals("action"))
                    .First().ParameterValue);
            }

            Type action = assembly.GetType(ActionHelper.GetActionLocation(
                this.ActionDefaultLocation, action_name));

            if (Utils.IsNotNull(action))
            {
                // Init action
                WebAction webAction = (WebAction)Activator.CreateInstance(action, webDataEngine);

                actionResult = webAction.Exec(action_action);
            }
            else
            {
                actionResult = action_name + " not found !";
            }

            return Content(actionResult);
        }

        [Route("frag")]
        [AllowCrossSiteJson]
        public virtual IActionResult GetFrag(string frag_name)
        {
            return this.GetFrag(frag_name, Assembly.GetExecutingAssembly());
        }

        protected IActionResult GetFrag(string frag_name, Assembly assembly)
        {
            IActionResult viewResult = new ContentResult();
            // Params of the request
            List<Parameter> parameters = ViewHelper.GetValuesFromUrl(this.Request.QueryString.Value);
            // If frag_name not found, try found viewName inside parameters
            if (Utils.IsEmpty(frag_name))
            {
                frag_name = Utils.GetAsString(parameters.Where(param => param.ParameterName.Equals("viewName"))
                    .First().ParameterValue);
            }
            // Get the view's type for instanciate new view
            Type view = assembly.GetType(ViewHelper.GetViewLocation(this.ViewDefaultLocation, frag_name));

            if (Utils.IsNull(view))
            {
                view = Assembly.GetExecutingAssembly()
                    .GetType(ViewHelper.GetViewLocation(ViewHelper.MainViewLocationPath, frag_name));
            }
            // The view
            WebView webView = null;

            if (Utils.IsNotNull(view))
            {
                // Init web data engine
                WebDataEngine webDataEngine = new WebDataEngine(this.HttpContext, this.DbContext);
                webDataEngine.AjaxParameters = parameters;
                // Call action before the view
                this.ExecAction("", "", assembly);
                // Create view instance
                webView = (WebView)Activator.CreateInstance(view, webDataEngine);
                webView.WebFileImport.ImportJs.Add(this.MainReactFile);
                // Store view in context for get data inside the view
                this.HttpContext.Items.Add("WebView", webView);
                // Load the html file with view
                viewResult = PartialView("/Views/" + frag_name + ".cshtml");

            }
            else
            {
                // View not found
                viewResult = Content(frag_name + " not found !");
            }

            return viewResult;
        }

        private JsonResult GetViewResult(PartialViewResult viewResult, WebView webView)
        {
            Dictionary<string, object> viewResultJson = new Dictionary<string, object>();

            viewResultJson.Add("HEAD", this.ConcatenateViewHeaders(webView.WebFileImport));
            viewResultJson.Add("BODY", viewResult);

            return this.GetJsonResult(viewResultJson);
        }

        private string ConcatenateViewHeaders(WebFileImport webFileImport)
        {
            StringBuilder sbHeaders = new StringBuilder();

            /*sbHeaders.Append(webFileImport.GetCssImport()).Append("\n")
                .Append(webFileImport.GetJavascriptImport()).Append("\n")
                .Append(webFileImport.GetJavascriptFunctions());*/

            return sbHeaders.ToString();
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

        protected abstract void InitDbContext();
    }
}
