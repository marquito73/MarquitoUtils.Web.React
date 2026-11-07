using MarquitoUtils.Web.React.Communication;
using MarquitoUtils.Web.React.Enums;
using System.Text;
using System.Web;

namespace MarquitoUtils.Web.React.Url
{
    public class WebAjaxUrl<T> : WebUrl where T : WebAjax
    {
        public string AjaxMainUrl { get; set; } = "/home/ajax";
        public Type Ajax { get; private set; } = typeof(T);
        public string AjaxAction { get; set; } = "";
        public WebAjaxUrl(string ajaxAction) : base(EnumUrlType.Ajax)
        {
            this.AjaxAction = ajaxAction;
        }

        public override string GetEncodedUrl()
        {
            StringBuilder sbEncodedUrl = new StringBuilder();

            sbEncodedUrl.Append(this.AjaxMainUrl)
                .Append("?").Append("urlType").Append("=").Append("ajax")
                .Append("&").Append("ajax_name").Append("=").Append(this.GetAjaxName().Trim())
                .Append("&").Append("ajax_action").Append("=").Append(this.AjaxAction.Trim());

            foreach (KeyValuePair<string, string> parameter in this.Parameters)
            {
                sbEncodedUrl.Append("&").Append(HttpUtility.UrlEncode(parameter.Key))
                    .Append("=").Append(HttpUtility.UrlEncode(parameter.Value));
            }

            return sbEncodedUrl.ToString();
        }

        public string GetAjaxName()
        {
            return this.Ajax.Name;
        }
    }
}
