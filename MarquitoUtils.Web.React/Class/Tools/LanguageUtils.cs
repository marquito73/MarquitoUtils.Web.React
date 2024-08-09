using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarquitoUtils.Main.Class.Enums.EnumLang;

namespace MarquitoUtils.Web.React.Class.Tools
{
    /// <summary>
    /// Class for manage languages
    /// </summary>
    public class LanguageUtils
    {
        /// <summary>
        /// Get the current language from the web context
        /// </summary>
        /// <param name="webContext">The web context</param>
        /// <returns>The current language from the web context</returns>
        public static string GetLanguage(HttpContext webContext)
        {
            return webContext.Request.Headers["Accept-Language"].ToString().Split(";")
                .FirstOrDefault()?.Split(",").FirstOrDefault();
        }

        /// <summary>
        /// Get the culture about a language as string
        /// </summary>
        /// <param name="language">A language as string</param>
        /// <returns>The culture about a language as string</returns>
        public static CultureInfo GetCultureLanguage(string language)
        {
            switch (language)
            {
                case "GB":
                    language = "en-gb";
                    break;
                case "US":
                    language = "us-gb";
                    break;
            }
            return CultureInfo.GetCultureInfo(language);
        }

        /// <summary>
        /// Get the current culture from the web context
        /// </summary>
        /// <param name="webContext">The web context</param>
        /// <returns>The current culture from the web context</returns>
        public static CultureInfo GetCultureLanguage(HttpContext webContext)
        {
            return CultureInfo.GetCultureInfo(GetLanguage(webContext));
        }

        public static enumLang GetLanguage(CultureInfo culture)
        {
            string languageISOcode = culture.TwoLetterISOLanguageName;

            return Enum.GetValues(typeof(enumLang))
                .Cast<enumLang>()
                .Where(lang => lang.ToString().ToUpper().Equals(languageISOcode.ToUpper()))
                .FirstOrDefault(enumLang.EN);
        }
    }
}
