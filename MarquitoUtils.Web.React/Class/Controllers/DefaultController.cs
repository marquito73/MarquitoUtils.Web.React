using MarquitoUtils.Main.Class.Entities.File;
using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Entities.Sql;
using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Main.Class.Service.Files;
using MarquitoUtils.Main.Class.Sql;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Attributes;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Communication.JSON;
using MarquitoUtils.Web.React.Class.Enums.Action;
using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;
using MarquitoUtils.Web.React.Class.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MarquitoUtils.Web.React.Class.Controllers
{
    /// <summary>
    /// Default controller
    /// </summary>
    [Route("home")]
    public abstract class DefaultController<T> : Controller
        where T : DefaultDbContext
    {
        /// <summary>
        /// Notify hub proxy, for communication with clients
        /// </summary>
        protected NotifyHubProxy NotifyHubProxy { get; private set; }
        protected ILogger<DefaultController<T>> _logger;
        /// <summary>
        /// Ajax default location
        /// </summary>
        protected string AjaxDefaultLocation { get; set; } = "";
        /// <summary>
        /// Ajax defaut location path
        /// </summary>
        protected string MainAjaxLocationPath { get; set; } = "MarquitoUtils.Web.React.Class.Ajax";
        protected Assembly MainAssembly { get; set; } = Assembly.GetExecutingAssembly();
        /// <summary>
        /// Action default location
        /// </summary>
        protected string ActionDefaultLocation { get; set; } = "";
        /// <summary>
        /// View default location
        /// </summary>
        protected string ViewDefaultLocation { get; set; } = "";
        /// <summary>
        /// Main react file
        /// </summary>
        protected string MainReactFile { get; set; } = "/dist/Main";
        /// <summary>
        /// Db context
        /// </summary>
        protected DefaultDbContext DbContext { get; set; }
        private IFileService FileService { get; set; } = new FileService();

        /// <summary>
        /// Default controller
        /// </summary>
        /// <param name="logger">Logger</param>
        protected DefaultController(ILogger<DefaultController<T>> logger)
        {
            this._logger = logger;

            string entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;

            this.AjaxDefaultLocation = $"{entryAssemblyName}.Ajax";
            this.ActionDefaultLocation = $"{entryAssemblyName}.Action";
            this.ViewDefaultLocation = $"{entryAssemblyName}.Views";
        }

        /// <summary>
        /// Default controller
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="notifyHub">Notify hub</param>
        protected DefaultController(ILogger<DefaultController<T>> logger, IHubContext<NotifyHub.NotifyHub> notifyHub) : this(logger)
        {
            this.NotifyHubProxy = new NotifyHubProxy(notifyHub);
        }

        /// <summary>
        /// Init database context
        /// </summary>
        private void InitDbContext()
        {
            if (Utils.IsNull(this.DbContext))
            {
                DatabaseConfiguration databaseConfiguration =
                this.FileService.GetDefaultDatabaseConfiguration();

                this.DbContext = DefaultDbContext.GetDbContext<T>(databaseConfiguration);
            }
        }

        /// <summary>
        /// Change the language for the current user
        /// </summary>
        /// <param name="newLanguage">The new language to display</param>
        /// <returns></returns>
        [Route("language")]
        [AllowCrossSiteJson]
        public JsonResult ChangeLanguage(string newLanguage)
        {
            WebDataEngine dataEngine = this.GetWebDataEngine();

            dataEngine.SetSessionValue("CURRENT_LANGUAGE", LanguageUtils.GetCultureLanguage(newLanguage));

            //return this.GetJsonResult(new { Reload = true });

            return this.GetSuccessJsonResult("", new
            {
                Reload = true,
            });
        }

        /// <summary>
        /// Return index
        /// </summary>
        /// <returns>The index view</returns>
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public virtual IActionResult Index()
        {
            return this.GetView("Home/Index");
        }

        /// <summary>
        /// Get web data engine
        /// </summary>
        /// <param name="parameters">Parameters from POST or GET query</param>
        /// <returns></returns>
        private WebDataEngine GetWebDataEngine(List<Parameter> parameters = null)
        {
            WebDataEngine webDataEngine = new WebDataEngine(this.HttpContext, this.DbContext);

            webDataEngine.ControllerContext = this.ControllerContext;
            webDataEngine.ControllerViewData = this.ViewData;
            webDataEngine.ControllerTempData = this.TempData;

            webDataEngine.AjaxParameters = Utils.Nvl(parameters);


            if (this.Request.HasFormContentType && Utils.IsNotNull(this.Request.Form))
            {
                // Get files if we have it
                if (Utils.IsNotEmpty(this.Request.Form.Files))
                {
                    webDataEngine.Files = this.Request.Form.Files.ToList();
                }
                // Get form data if we have it
                this.Request.Form.Where(formData =>
                {
                    return formData.Key == "form";
                }).Select(formData =>
                {
                    return formData.Value;
                }).ToList().ForEach(formData =>
                {
                    webDataEngine.FormParameters = Utils.GetDeserializedObject<Dictionary<string, object>>(formData)
                        .Select(parameter =>
                        {
                            return new Parameter(parameter.Key, parameter.Value);
                        }).ToList();
                });

            }

            return webDataEngine;
        }

        /// <summary>
        /// Exec ajax call, and return result
        /// </summary>
        /// <returns>Result of ajax call</returns>
        [Route("ajax")]
        [RequestSizeLimit(1_000_000_000)]
        [AllowCrossSiteJson]
        public virtual IActionResult ExecAjax()
        {
            return this.ExecAjax(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Exec ajax call, and return result
        /// </summary>
        /// <param name="assembly">The assembly where the ajax is located</param>
        /// <returns>Result of ajax call</returns>
        protected IActionResult ExecAjax(Assembly assembly)
        {
            IActionResult ajaxResult = new ContentResult();

            this.InitDbContext();

            // Parameters in query
            List<Parameter> parameters = AjaxHelper.GetValuesFromUrl(this.Request.QueryString.Value);
            // Init web data engine
            WebDataEngine webDataEngine = this.GetWebDataEngine(parameters);
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
                WebAjax webAjax = (WebAjax)Activator.CreateInstance(ajax, webDataEngine, this.NotifyHubProxy);
                // Execute the ajax
                ajaxResult = webAjax.Exec(ajax_action);
            }
            else
            {
                ajaxResult = Content(ajax_name + " not found !");
            }

            return ajaxResult;
        }

        /// <summary>
        /// Exec action call, and return result
        /// </summary>
        /// <param name="action_name">Action name</param>
        /// <param name="action_action">Action action</param>
        /// <returns>The action result</returns>
        [Route("action")]
        [AllowCrossSiteJson]
        public virtual IActionResult ExecAction(string action_name, string action_action)
        {
            return this.ExecAction(action_name, action_action, Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Exec action call, and return result
        /// </summary>
        /// <param name="action_name">Action name</param>
        /// <param name="action_action">Action action</param>
        /// <param name="assembly">The assembly where the action is located</param>
        /// <returns>The action result</returns>
        protected IActionResult ExecAction(string action_name, string action_action, Assembly assembly)
        {
            IActionResult actionResult = new ContentResult();

            this.InitDbContext();

            List<Parameter> parameters = ActionHelper.GetValuesFromUrl(this.Request.QueryString.Value);

            // Init web data engine
            WebDataEngine webDataEngine = this.GetWebDataEngine(parameters);

            Type action = null;
            if (Utils.IsNotEmpty(parameters))
            {
                // If action_name or action not found, try found actionName and action inside parameters
                if ((Utils.IsEmpty(action_name) || Utils.IsEmpty(action_action)))
                {
                    action_name = Utils.GetAsString(parameters.Where(param => param.ParameterName.Equals("actionName"))
                        .First().ParameterValue);
                    action_action = Utils.GetAsString(parameters.Where(param => param.ParameterName.Equals("action"))
                        .First().ParameterValue);
                }

                IEnumerable<Parameter> actionFullNameParameter = parameters
                    .Where(param => param.ParameterName == "actionFullName");

                if (Utils.IsNotEmpty(actionFullNameParameter))
                {
                    action = assembly.GetType(Utils.GetAsString(actionFullNameParameter.First().ParameterValue));
                }
            }

            if (Utils.IsNull(action))
            {
                action = assembly.GetType(ActionHelper.GetActionLocation(
                    this.ActionDefaultLocation, action_name));
            }
            // TODO Faire le path relatif a la vue

            if (Utils.IsNotNull(action))
            {
                // Init action
                WebAction webAction = (WebAction)Activator.CreateInstance(action, webDataEngine, this.NotifyHubProxy);

                actionResult = webAction.Exec(EnumUtils.GetEnum<EnumAction>(action_action));
            }
            else
            {
                actionResult = this.GetContentResult($"{action_name} not found !");
            }

            return actionResult;
        }

        /// <summary>
        /// Get view
        /// </summary>
        /// <param name="frag_name">View's name</param>
        /// <returns>A view</returns>
        [Route("frag")]
        [AllowCrossSiteJson]
        public virtual IActionResult GetView(string frag_name)
        {
            return this.GetView(frag_name, Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Get view
        /// </summary>
        /// <param name="frag_name">View's name</param>
        /// <param name="assembly">The assembly where the view is located</param>
        /// <returns>A view</returns>
        protected IActionResult GetView(string frag_name, Assembly assembly)
        {
            IActionResult viewResult = new ContentResult();

            this.InitDbContext();

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
                WebDataEngine webDataEngine = this.GetWebDataEngine(parameters);
                // Call action before the view
                this.ExecAction("", "", assembly);
                // Create view instance
                webView = (WebView)Activator.CreateInstance(view, webDataEngine, this.NotifyHubProxy);
                this.AddMainReactFileToView(webView);
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

        private void AddMainReactFileToView(WebView webView)
        {
            webView.WebFileImport.JavascriptModules.Add(this.MainReactFile, new List<string>());
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

        /// <summary>
        /// Get JSON result
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns>JSON content result</returns>
        protected JsonResult GetJsonResult(object content)
        {
            return new JsonResult(content);
        }

        /// <summary>
        /// Get file result
        /// </summary>
        /// <param name="fileBytes">The file content</param>
        /// <param name="fileName">The file name</param>
        /// <returns>File result</returns>
        protected FileContentResult GetFileResult(byte[] fileBytes, string fileName)
        {
            FileContentResult result = new FileContentResult(fileBytes, "application/octet-stream");

            result.FileDownloadName = fileName;

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
    }
}
