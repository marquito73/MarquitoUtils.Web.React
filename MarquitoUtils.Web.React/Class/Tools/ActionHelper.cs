using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Tools;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MarquitoUtils.Web.React.Class.Tools
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
