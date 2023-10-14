using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class WebUtils : Utils
    {
        public static string GetJavaScriptFunction(string jsFunctionName, params object[] jsParams)
        {
            StringBuilder sbJsFunction = new StringBuilder();

            sbJsFunction.Append(jsFunctionName).Append("(");

            int count = 0;
            foreach (object jsParam in jsParams)
            {
                if (count > 0)
                {
                    sbJsFunction.Append(", ");
                }

                if (jsParam is string)
                {
                    sbJsFunction.Append("\"").Append(jsParam).Append("\"");
                }
                else if(jsParam is WebString)
                {
                    sbJsFunction.Append(((WebString)jsParam).ToString());
                }
                else if (jsParam is EnumClass)
                {
                    sbJsFunction.Append(((EnumClass)jsParam).ToString());
                }
                else if (jsParam is bool)
                {
                    sbJsFunction.Append(((bool)jsParam).ToString().ToLower());
                }
                else
                {
                    sbJsFunction.Append(GetAsString(jsParam));
                }

                count++;
            }

            sbJsFunction.Append(")");

            return sbJsFunction.ToString();
        }

        public static string GetJQueryFunction(string jQuerySelector, string function, 
            params object[] jsParams)
        {
            string jQueryFunction = "$(\"" + jQuerySelector + "\")." + function;

            return GetJavaScriptFunction(jQueryFunction, jsParams);
        }

        public static WebString GetParametersParsed(object objectToParse)
        {
            string result = GetSerializedObject(objectToParse);

            WebString webResult = new WebString("JSON.parse('" + result + "')");
            webResult.NeedQuotes = false;

            return webResult;
        }

        /// <summary>
        /// Get the correct fetch root url
        /// </summary>
        /// <param name="rootUrl">The root url</param>
        /// <returns>The correct fetch root url</returns>
        public static string GetCorrectFecthRootUrl(string rootUrl)
        {
            if (rootUrl.Contains("localhost:"))
            {
                if (rootUrl.Contains("/"))
                {
                    rootUrl = rootUrl.Substring(rootUrl.IndexOf("/"));
                }
                else
                {
                    rootUrl = "";
                }
            }

            return rootUrl;
        }
    }
}
