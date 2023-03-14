namespace MarquitoUtils.Web.React.Class.Tools
{
    public class AjaxHelper : WebHelper
    {
        public static string GetAjaxLocation(string ajaxLocationPath, string ajaxName)
        {
            return GetWebLocation(ajaxLocationPath, ajaxName);
        }

        public static string GetAjaxLocation(string ajaxLocationPath, string ajaxName, 
            string ajaxCompletionLocation)
        {
            return GetWebLocation(ajaxLocationPath, ajaxName, ajaxCompletionLocation);
        }
    }
}
