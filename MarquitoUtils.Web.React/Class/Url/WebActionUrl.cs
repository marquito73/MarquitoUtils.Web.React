using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MarquitoUtils.Web.React.Class.Url
{
    public class WebActionUrl<T> : WebUrl where T : WebAction
    {
        public string ActionMainUrl { get; set; } = "/home/action";
        public Type Action { get; private set; } = typeof(T);
        public string ActionAction { get; set; } = "";
        public WebActionUrl(string action) : base(EnumUrlType.Action)
        {
            this.ActionAction = action;
        }

        public override string GetEncodedUrl()
        {
            StringBuilder sbEncodedUrl = new StringBuilder();

            sbEncodedUrl.Append(this.ActionMainUrl)
                .Append("?").Append("actionName").Append("=").Append(this.GetActionName().Trim())
                .Append("&").Append("action").Append("=").Append(this.ActionAction.Trim());

            foreach (KeyValuePair<string, string> parameter in this.Parameters)
            {
                sbEncodedUrl.Append("&").Append(HttpUtility.UrlEncode(parameter.Key))
                    .Append("=").Append(HttpUtility.UrlEncode(parameter.Value));
            }

            return sbEncodedUrl.ToString();
        }

        public string GetActionName()
        {
            return this.Action.Name;
        }
    }
}
