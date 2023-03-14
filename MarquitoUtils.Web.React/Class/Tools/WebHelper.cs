using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Tools;
using System.Text;
using System.Web;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class WebHelper
    {
        private static string GetCompletionLocationFromList(List<string> webList)
        {
            StringBuilder completionLocation = new StringBuilder();

            foreach (string webElem in webList)
            {
                if (!webElem.Equals(webList.Last()))
                {
                    if (Utils.IsNotEmpty(completionLocation.ToString()))
                    {
                        completionLocation.Append(".");
                    }
                    completionLocation.Append(webElem);
                }
            }

            return completionLocation.ToString();
        }

        protected static string GetWebLocation(string webLocationPath, string webName)
        {
            List<string> webList = Utils.ExtractStringList(webName, '/');

            string finalWebName = webList.Last();

            string completion = GetCompletionLocationFromList(webList);

            return GetWebLocation(webLocationPath, finalWebName, completion);
        }

        protected static string GetWebLocation(string webLocationPath, string webName,
            string webCompletionLocation)
        {
            StringBuilder sbWeb = new StringBuilder();

            sbWeb.Append(webLocationPath);

            if (Utils.IsNotEmpty(webCompletionLocation))
            {
                sbWeb.Append(".")
                    .Append(webCompletionLocation);
            }

            sbWeb.Append(".")
                .Append(webName);

            return sbWeb.ToString();
        }

        public static List<Parameter> GetValuesFromUrl(string url)
        {
            List<Parameter> values = new List<Parameter>();

            if (Utils.IsNotEmpty(url))
            {
                //foreach (string param in url.Replace("?", " ").Split("&~"))
                foreach (string param in url.Substring(1, url.Length - 1).Split("&"))
                {
                    string decodedParam = HttpUtility.UrlDecode(param);

                    string[] decodedParams = decodedParam.Split("=", 2);

                    if (decodedParams.Length > 1)
                    {
                        string name = decodedParams[0];

                        string value = decodedParams[1];

                        if (Utils.IsNotEmpty(name))
                        {
                            Parameter parameter = new Parameter(name, value);
                            values.Add(parameter);
                        }
                    }
                }
            }

            return values;
        }
    }
}
