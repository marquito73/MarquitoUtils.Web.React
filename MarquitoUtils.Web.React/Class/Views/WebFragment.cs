using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Views
{
    public abstract class WebFragment : WebView
    {
        protected WebFragment(WebDataEngine webDataEngine, NotifyHubProxy notifyHubProxy) : base(webDataEngine, notifyHubProxy)
        {
        }

        protected WebFragment(HttpContext webContext, NotifyHubProxy notifyHubProxy) : base(webContext, notifyHubProxy)
        {
        }

        public ActionResult GetView()
        {
            //ViewEngine.Engines
            // Store view in context for get data inside the view
            this.WebDataEngine.WebContext.Items.Add(this.GetType().Name, this);
            // Load the html file with view
            /*return new PartialViewResult()
            {
                ViewName = $"/Views/Home/{this.GetType().Name}.cshtml",
            };*/

            return this.GetContentResult("Test");
        }
    }
}
