using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MarquitoUtils.Web.React.Class.Views
{
    /// <summary>
    /// Class for render fragment of cshtml inside another view
    /// </summary>
    public abstract class WebFragment : WebView
    {
        /// <summary>
        /// Class for render fragment of cshtml inside another view
        /// </summary>
        /// <param name="webDataEngine">Web engine, contain data about session</param>
        protected WebFragment(WebDataEngine webDataEngine) : base(webDataEngine)
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
            // The view find
            ViewEngineResult viewResult = this.GetViewResult();

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

        /// <summary>
        /// Return the view result
        /// </summary>
        /// <returns>The view result</returns>
        private ViewEngineResult GetViewResult()
        {
            // The view name
            string viewName = FileHelper.GetRelativePathOfClass(this.GetType(), ".Views.");
            // The view path
            string viewNamePath = $"{viewName}.cshtml";
            // The view engine, for find views
            IViewEngine viewEngine = this.WebDataEngine.WebContext.RequestServices
                .GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            // The view find
            return viewEngine.GetView(null, viewNamePath, false);
        }
    }
}
