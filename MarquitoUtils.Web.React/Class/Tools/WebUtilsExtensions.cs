using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Views;
using Microsoft.AspNetCore.Http;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public static class WebUtilsExtensions
    {
        public static TView GetView<TView>(this HttpContext webContext)
            where TView : WebView
        {
            return (TView) webContext.Items[WebView.VIEW_NAME];
        }

        public static WebDictionnary<TKey, TValue> ToWebDictionnary<TKey, TValue>(this Dictionary<TKey, TValue> map)
        {
            WebDictionnary<TKey, TValue> webMap = new WebDictionnary<TKey, TValue>();

            map.ToList().ForEach(x => webMap.Add(x.Key, x.Value));

            return webMap;
        }
    }
}
