using MarquitoUtils.Web.React.Class.Views;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public static class WebUtilsExtensions
    {
        public static TView GetView<TView>(this HttpContext webContext)
            where TView : WebView
        {
            return (TView) webContext.Items[WebView.VIEW_NAME];
        }
    }
}
