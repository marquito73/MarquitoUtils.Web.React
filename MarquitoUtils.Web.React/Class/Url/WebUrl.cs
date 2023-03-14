using MarquitoUtils.Main.Class.Converters;
using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Enums;
using Newtonsoft.Json;

namespace MarquitoUtils.Web.React.Class.Url
{
    /// <summary>
    /// A web url
    /// </summary>
    /// 
    [JsonConverter(typeof(ToStringJsonConverter))]
    public abstract class WebUrl
    {
        /// <summary>
        /// A dictionary of url's parameters, with param's name for Key, param's value for Value,
        /// incorporated inside url
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// A dictionary of url's parameters, with param's name for Key, param's value for Value,
        /// not incorporated inside url
        /// </summary>
        public Dictionary<string, string> OtherParameters { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Url type, default for call WebView
        /// </summary>
        public EnumUrlType UrlType { get; private set; } = EnumUrlType.View;
        /// <summary>
        /// A web url
        /// </summary>
        /// <param name="urlType">Url type</param>
        public WebUrl(EnumUrlType urlType)
        {
            this.UrlType = urlType;
        }

        public abstract string GetEncodedUrl();



        public override string ToString()
        {
            return new WebString(this.GetEncodedUrl(), true).ToString();
        }
    }
}
