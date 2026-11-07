namespace MarquitoUtils.Web.React.Tools
{
    public class ActionHelper : WebHelper
    {
        public static string GetActionLocation(string actionLocationPath, string actionName)
        {
            return GetActionLocation(actionLocationPath, actionName, "");
        }

        public static string GetActionLocation(string actionLocationPath, string actionName,
            string actionCompletionLocation)
        {
            return GetWebLocation(actionLocationPath, actionName, actionCompletionLocation);
        }
    }
}
