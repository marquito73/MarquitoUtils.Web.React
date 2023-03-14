namespace MarquitoUtils.Web.React.Class.Tools
{
    public class ViewHelper : WebHelper
    {
        public static string MainViewLocationPath = "MarquitoUtils.Web.Class.View";
        public static string GetViewLocation(string viewLocationPath, string viewName)
        {
            return GetWebLocation(viewLocationPath, viewName);
        }

        public static string GetViewLocation(string viewLocationPath, string viewName,
            string viewCompletionLocation)
        {
            return GetViewLocation(viewLocationPath, viewName, viewCompletionLocation);
        }
    }
}
