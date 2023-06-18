using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Views
{
    /// <summary>
    /// Class for render fragment of cshtml inside another view
    /// </summary>
    public abstract class WebFragment : WebView
    {
        protected WebFragment(WebDataEngine webDataEngine, NotifyHubProxy notifyHubProxy) : base(webDataEngine, notifyHubProxy)
        {
        }

        /// <summary>
        /// Render and return the view as html
        /// </summary>
        /// <returns>The view rendered as html</returns>
        public HtmlString GetView()
        {
            HtmlString viewHtmlResult = new HtmlString("");

            // Store view in context for get data inside the view
            this.WebDataEngine.WebContext.Items.Add(this.GetType().Name, this);
            // The view name
            string viewName = this.GetType().FullName.Substring(this.GetType().FullName.IndexOf(".Views.") + 1)
                .Replace(".", "/");
            // The view path
            string viewNamePath = $"{viewName}.cshtml";
            // The view engine, for find views
            IViewEngine viewEngine = this.WebDataEngine.WebContext.RequestServices
                .GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            // The view find
            ViewEngineResult viewResult = viewEngine.GetView(null, viewNamePath, false);

            using (var sw = new StringWriter())
            {
                // Get the result of the view as string
                ViewContext viewContext = new ViewContext(this.WebDataEngine.ControllerContext, viewResult.View, 
                    this.WebDataEngine.ControllerViewData, this.WebDataEngine.ControllerTempData, sw, new HtmlHelperOptions());
                viewResult.View.RenderAsync(viewContext);
                viewHtmlResult = new HtmlString(sw.ToString());
            }

            return viewHtmlResult;
        }
    }
}
