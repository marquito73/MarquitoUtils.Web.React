using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.Enums.Action;
using MarquitoUtils.Web.React.Class.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MarquitoUtils.Web.React.Class.Url
{
    public class WebViewUrl<V, A> : WebUrl 
        where V : WebView 
        where A : WebAction
    {
        public string ViewMainUrl { get; set; } = "/home/frag";
        public Type View { get; private set; } = typeof(V);
        public Type Action { get; private set; } = typeof(A);
        public EnumAction ActionAction { get; set; } = EnumAction.None;
        public WebViewUrl(EnumAction action) : base(EnumUrlType.View)
        {
            this.ActionAction = action;
        }

        public override string GetEncodedUrl()
        {
            StringBuilder sbEncodedUrl = new StringBuilder();

            sbEncodedUrl.Append(this.ViewMainUrl)
                .Append("?").Append("urlType").Append("=").Append("view")
                .Append("&").Append("viewName").Append("=").Append(this.GetViewFullName().Trim())
                .Append("&").Append("actionFullName").Append("=").Append(this.GetActionFullName().Trim())
                .Append("&").Append("actionName").Append("=").Append(this.GetActionName().Trim())
                .Append("&").Append("action").Append("=").Append(this.ActionAction.GetEnumName().Trim());

            foreach (KeyValuePair<string, string> parameter in this.Parameters)
            {
                sbEncodedUrl.Append("&").Append(HttpUtility.UrlEncode(parameter.Key))
                    .Append("=").Append(HttpUtility.UrlEncode(parameter.Value));
            }

            return sbEncodedUrl.ToString();
        }

        public string GetViewName()
        {
            return this.View.Name;
        }

        public string GetActionFullName()
        {
            return this.Action.FullName;
        }

        public string GetActionName()
        {
            return this.Action.Name;
        }

        public string GetViewFullName()
        {

            //int firstOccur = this.View.FullName.IndexOf(".") + 1;
            int firstOccur = this.View.FullName.IndexOf(".Views.") + ".Views.".Length;

            return this.View.FullName.Substring(firstOccur).Replace(".", "/");
        }
    }
}
