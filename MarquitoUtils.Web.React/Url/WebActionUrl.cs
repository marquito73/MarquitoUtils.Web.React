using MarquitoUtils.Main.Common.Enums;
using MarquitoUtils.Web.React.Communication;
using MarquitoUtils.Web.React.Enums;
using MarquitoUtils.Web.React.Enums.Action;
using System.Text;
using System.Web;

namespace MarquitoUtils.Web.React.Url
{
    public class WebActionUrl<T> : WebUrl where T : WebAction
    {
        public string ActionMainUrl { get; set; } = "/home/action";
        public Type Action { get; private set; } = typeof(T);
        public EnumAction ActionAction { get; set; } = EnumAction.None;
        public WebActionUrl(EnumAction action) : base(EnumUrlType.Action)
        {
            this.ActionAction = action;
        }

        public override string GetEncodedUrl()
        {
            StringBuilder sbEncodedUrl = new StringBuilder();

            sbEncodedUrl.Append(this.ActionMainUrl)
                .Append("?").Append("actionName").Append("=").Append(this.GetActionName().Trim())
                .Append("&").Append("actionFullName").Append("=").Append(this.GetActionFullName().Trim())
                .Append("&").Append("action").Append("=").Append(this.ActionAction.GetEnumName().Trim());

            foreach (KeyValuePair<string, string> parameter in this.Parameters)
            {
                sbEncodedUrl.Append("&").Append(HttpUtility.UrlEncode(parameter.Key))
                    .Append("=").Append(HttpUtility.UrlEncode(parameter.Value));
            }

            return sbEncodedUrl.ToString();
        }

        public string GetActionFullName()
        {
            return this.Action.FullName;
        }

        public string GetActionName()
        {
            return this.Action.Name;
        }
    }
}
