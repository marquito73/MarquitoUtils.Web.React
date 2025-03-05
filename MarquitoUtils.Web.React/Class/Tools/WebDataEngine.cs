using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Main.Class.Sql;
using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace MarquitoUtils.Web.React.Class.Tools
{
    /// <summary>
    /// Web engine, for manage data in session
    /// </summary>
    public class WebDataEngine
    {
        private readonly string WebSessionMapName = "MAIN_SESSION_MAP";
        public static readonly string LanguageSessionKey = "CURRENT_LANGUAGE";
        /// <summary>
        /// Web context
        /// </summary>
        public HttpContext WebContext { get; set; }
        /// <summary>
        /// The session
        /// </summary>
        private ISession WebSession { get; set; }
        /// <summary>
        /// The root url of the current session
        /// </summary>
        public string RootUrl { get; private set; }
        /// <summary>
        /// The Database Context
        /// </summary>
        public DefaultDbContext DbContext { get; private set; }
        /// <summary>
        /// The parameters from the query
        /// </summary>
        public List<Parameter> AjaxParameters { get; set; } = new List<Parameter>();
        /// <summary>
        /// The parameters from the form
        /// </summary>
        public List<Parameter> FormParameters { get; set; } = new List<Parameter>();
        /// <summary>
        /// Files list
        /// </summary>
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        /// <summary>
        /// Current language
        /// </summary>
        public CultureInfo CurrentLanguage {  get; private set; } = CultureInfo.CurrentCulture;
        /// <summary>
        /// Controller context
        /// </summary>
        public ControllerContext ControllerContext { get; set; }
        public ViewDataDictionary ControllerViewData { get; set; }
        public ITempDataDictionary ControllerTempData { get; set; }

        /// <summary>
        /// Web engine, for manage data in session
        /// </summary>
        /// <param name="webSession">The web session</param>
        /// <param name="rootUrl">The root url</param>
        public WebDataEngine(ISession webSession, string rootUrl)
        {
            // Set the session
            this.WebSession = webSession;
            // Set the root url of the current session
            this.RootUrl = rootUrl;
            // Init session map if not set
            this.InitSessionMainMap();
        }

        /// <summary>
        /// Web engine, for manage data in session
        /// </summary>
        /// <param name="webContext">The web context</param>
        public WebDataEngine(HttpContext webContext) 
            : this(webContext.Session, webContext.Request.Host.Value)
        {
            this.WebContext = webContext;
            string cultureInfoCode = webContext.Request.Headers["Accept-Language"].ToString().Split(";")
                .FirstOrDefault()?.Split(",").FirstOrDefault();
            this.CurrentLanguage = CultureInfo.GetCultureInfo(cultureInfoCode);
        }

        /// <summary>
        /// Web engine, for manage data in session
        /// </summary>
        /// <param name="webSession">The web session</param>
        /// <param name="dbContext">The database context</param>
        /// <param name="rootUrl">The root url</param>
        public WebDataEngine(ISession webSession, DefaultDbContext dbContext, string rootUrl) 
            : this(webSession, rootUrl)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Web engine, for manage data in session
        /// </summary>
        /// <param name="webContext">The web context</param>
        /// <param name="dbContext">The database context</param>
        public WebDataEngine(HttpContext webContext, DefaultDbContext dbContext) 
            : this(webContext.Session, dbContext, webContext.Request.Host.Value)
        {
            this.WebContext = webContext;

            string language = this.GetSessionValue<string>(LanguageSessionKey);
            if (Utils.IsEmpty(language))
            {
                this.SetSessionValue("CURRENT_LANGUAGE", LanguageUtils.GetCultureLanguage(this.WebContext));
                language = this.GetSessionValue<string>(LanguageSessionKey);
            }
            this.CurrentLanguage = LanguageUtils.GetCultureLanguage(language);
        }

        #region Session methods

        /// <summary>
        /// Get session ID
        /// </summary>
        /// <returns>Session ID</returns>
        public string GetSessionID()
        {
            return this.WebSession.Id;
        }

        /// <summary>
        /// Get the user's IP address
        /// </summary>
        /// <returns>The user's IP address</returns>
        public string GetUserIPAddress()
        {
            return this.WebContext.Connection.RemoteIpAddress?.ToString();
        }

        /// <summary>
        /// Init session map
        /// </summary>
        private void InitSessionMainMap()
        {
            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            if (Utils.IsNull(sessionMap))
            {
                sessionMap = new Dictionary<string, object>();

                this.WebSession.SetString(this.WebSessionMapName, Utils.GetSerializedObject(sessionMap));
            }
        }

        /// <summary>
        /// Update session map
        /// </summary>
        /// <param name="sessionMap">The session map to set</param>
        private void UpdateSessionMainMap(Dictionary<string, object> sessionMap)
        {
            this.WebSession.SetString(this.WebSessionMapName, Utils.GetSerializedObject(sessionMap));
        }

        /// <summary>
        /// The session map
        /// </summary>
        /// <returns>Session map</returns>
        public Dictionary<string, object> GetSessionMainMap()
        {
            Dictionary<string, object> sessionMap = null;

            byte[] byteValue = this.WebSession.Get(this.WebSessionMapName);

            string sMap = this.WebSession.GetString(this.WebSessionMapName);

            if (Utils.IsNotNull(byteValue))
            {
                sessionMap = Utils.GetDeserializedObject<Dictionary<string, object>>(sMap);
            }

            return sessionMap;
        }

        /// <summary>
        /// Set if the user is authenticated or not
        /// </summary>
        /// <param name="authenticated">The user is authenticated ?</param>
        public void SetUserAuthenticated(bool authenticated)
        {
            this.SetSessionValue("IsAuthenticated", authenticated);
        }

        /// <summary>
        /// The user is authenticated ?
        /// </summary>
        /// <returns>The user is authenticated ?</returns>
        public bool IsUserAuthenticated()
        {
            return this.GetSessionValue<bool>("IsAuthenticated");
        }

        /// <summary>
        /// Get session value
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="varName">The name for found the value</param>
        /// <returns>Session value</returns>
        public T GetSessionValue<T>(string varName)
        {

            object value = null;

            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            if (Utils.IsNotEmpty(sessionMap))
            {
                sessionMap.TryGetValue(varName, out value);
            }

            T result = default(T);

            if (Utils.IsNotNull(value))
            {
                if (value.GetType() == typeof(T))
                {
                    result = (T)value;
                }
                else
                {
                    result = Utils.GetDeserializedObject<T>(Utils.GetAsString(value));
                }
            }

            return result;
        }

        /// <summary>
        /// Set session value
        /// </summary>
        /// <param name="varName">The name to store the value</param>
        /// <param name="var">Session value</param>
        public void SetSessionValue(string varName, object var)
        {
            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            if (sessionMap.ContainsKey(varName))
            {
                sessionMap.Remove(varName);
            }
            sessionMap.Add(varName, var);

            this.UpdateSessionMainMap(sessionMap);
        }

        /// <summary>
        /// Remove value from session
        /// </summary>
        /// <param name="varName">The name to store the value</param>
        public void RemoveSessionValue(string varName)
        {
            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            sessionMap.Remove(varName);

            this.UpdateSessionMainMap(sessionMap);
        }

        #endregion

        #region Get data from query

        /// <summary>
        /// Get string from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>String for the parameter name</returns>
        public string GetStringFromQuery(string parameterName)
        {
            return this.GetStringFromQueryOrForm(this.AjaxParameters, parameterName);
        }

        /// <summary>
        /// Get integer from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Integer for the parameter name</returns>
        public int GetIntegerFromQuery(string parameterName)
        {
            return this.GetIntegerFromQueryOrForm(this.AjaxParameters, parameterName);
        }

        /// <summary>
        /// Get double from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Double for the parameter name</returns>
        public double GetDoubleFromQuery(string parameterName)
        {
            return this.GetDoubleFromQueryOrForm(this.AjaxParameters, parameterName);
        }

        /// <summary>
        /// Get long from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Long for the parameter name</returns>
        public long GetLongFromQuery(string parameterName)
        {
            return this.GetLongFromQueryOrForm(this.AjaxParameters, parameterName);
        }

        /// <summary>
        /// Get boolean from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Boolean for the parameter name</returns>
        public bool GetBooleanFromQuery(string parameterName)
        {
            return this.GetBooleanFromQueryOrForm(this.AjaxParameters, parameterName);
        }

        public T GetFromQuery<T>(string parameterName)
            where T : class, new()
        {
            return this.GetFromQueryOrForm<T>(this.AjaxParameters, parameterName);
        }

        public TEnum? GetEnumFromQuery<TEnum>(string parameterName)
            where TEnum : struct, Enum
        {
            return this.GetEnumFromQueryOrForm<TEnum>(this.AjaxParameters, parameterName);
        }

        /// <summary>
        /// Get generic list from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic list for the parameter name</returns>
        public List<T> GetListFromQuery<T>(string parameterName)
        {
            return this.GetListFromQueryOrForm<T>(this.AjaxParameters, parameterName);
        }

        /// <summary>
        /// Get generic dictionary from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic dictionary for the parameter name</returns>
        public Dictionary<K,V> GetDictionaryFromQuery<K,V>(string parameterName)
        {
            return this.GetDictionaryFromQueryOrForm<K,V>(this.AjaxParameters, parameterName);
        }

        #endregion Get data from query

        #region Get data from form

        /// <summary>
        /// Get string from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>String for the parameter name</returns>
        public string GetStringFromForm(string parameterName)
        {
            return this.GetStringFromQueryOrForm(this.FormParameters, parameterName);
        }

        /// <summary>
        /// Get integer from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Integer for the parameter name</returns>
        public int GetIntegerFromForm(string parameterName)
        {
            return this.GetIntegerFromQueryOrForm(this.FormParameters, parameterName);
        }

        /// <summary>
        /// Get double from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Double for the parameter name</returns>
        public double GetDoubleFromForm(string parameterName)
        {
            return this.GetDoubleFromQueryOrForm(this.FormParameters, parameterName);
        }

        /// <summary>
        /// Get long from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Long for the parameter name</returns>
        public long GetLongFromForm(string parameterName)
        {
            return this.GetLongFromQueryOrForm(this.FormParameters, parameterName);
        }

        /// <summary>
        /// Get boolean from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Boolean for the parameter name</returns>
        public bool GetBooleanFromForm(string parameterName)
        {
            return this.GetBooleanFromQueryOrForm(this.FormParameters, parameterName);
        }

        public T GetFromForm<T>(string parameterName)
            where T : class, new()
        {
            return this.GetFromQueryOrForm<T>(this.FormParameters, parameterName);
        }

        public TEnum? GetEnumFromForm<TEnum>(string parameterName)
            where TEnum : struct, Enum
        {
            return this.GetEnumFromQueryOrForm<TEnum>(this.FormParameters, parameterName);
        }

        /// <summary>
        /// Get generic list from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic list for the parameter name</returns>
        public List<T> GetListFromForm<T>(string parameterName)
        {
            return this.GetListFromQueryOrForm<T>(this.FormParameters, parameterName);
        }

        /// <summary>
        /// Get generic dictionary from form
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic dictionary for the parameter name</returns>
        public Dictionary<K, V> GetDictionaryFromForm<K, V>(string parameterName)
        {
            return this.GetDictionaryFromQueryOrForm<K, V>(this.FormParameters, parameterName);
        }

        #endregion Get data from form

        #region Common for get data between query and form

        /// <summary>
        /// Get string from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>String for the parameter name</returns>
        private string GetStringFromQueryOrForm(List<Parameter> parameters, string parameterName)
        {
            string result = "";

            // The parameter found
            Parameter foundParameter = parameters.Find(parameter => parameter.ParameterName.Equals(parameterName));

            if (Utils.IsNotNull(foundParameter))
            {
                result = Utils.GetAsString(foundParameter.ParameterValue);
            }

            return result;
        }

        /// <summary>
        /// Get integer from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Integer for the parameter name</returns>
        private int GetIntegerFromQueryOrForm(List<Parameter> parameters, string parameterName)
        {
            int result = 0;

            string stringvalue = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsInteger(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get double from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Double for the parameter name</returns>
        private double GetDoubleFromQueryOrForm(List<Parameter> parameters, string parameterName)
        {
            double result = 0;

            string stringvalue = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsDouble(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get long from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Long for the parameter name</returns>
        private long GetLongFromQueryOrForm(List<Parameter> parameters, string parameterName)
        {
            long result = 0;

            string stringvalue = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsLong(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get boolean from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Boolean for the parameter name</returns>
        private bool GetBooleanFromQueryOrForm(List<Parameter> parameters, string parameterName)
        {
            bool result = false;

            string stringvalue = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsBoolean(stringvalue);
            }

            return result;
        }

        private T GetFromQueryOrForm<T>(List<Parameter> parameters, string parameterName)
            where T : class, new()
        {
            T result = new T();

            string stringvalue = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = stringvalue as T;
            }

            return result;
        }

        private TEnum? GetEnumFromQueryOrForm<TEnum>(List<Parameter> parameters, string parameterName)
            where TEnum : struct, Enum
        {
            TEnum? result = null;

            string stringvalue = this.GetStringFromQueryOrForm(parameters, parameterName);
            if (Utils.IsNotNull(stringvalue))
            {
                result = EnumUtils.GetEnum<TEnum>(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get generic list from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic list for the parameter name</returns>
        private List<T> GetListFromQueryOrForm<T>(List<Parameter> parameters, string parameterName)
        {
            List<T> resultList = new List<T>();

            string listString = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotEmpty(listString))
            {
                resultList = Utils.GetDeserializedObject<List<T>>(listString);
            }

            return resultList;
        }

        /// <summary>
        /// Get generic dictionary from query or form
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic dictionary for the parameter name</returns>
        private Dictionary<K, V> GetDictionaryFromQueryOrForm<K, V>(List<Parameter> parameters, string parameterName)
        {
            Dictionary<K, V> resultDictionary = new Dictionary<K, V>();

            string dictionaryString = this.GetStringFromQueryOrForm(parameters, parameterName);

            if (Utils.IsNotEmpty(dictionaryString))
            {
                resultDictionary = Utils.GetDeserializedObject<Dictionary<K, V>>(dictionaryString);
            }

            return resultDictionary;
        }

        #endregion
    }
}
