using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Sql;
using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
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
            string cultureInfoCode = webContext.Request.Headers["Accept-Language"].ToString().Split(";")
                .FirstOrDefault()?.Split(",").FirstOrDefault();
            this.CurrentLanguage = CultureInfo.GetCultureInfo(cultureInfoCode);
        }

        /// <summary>
        /// Get session ID
        /// </summary>
        /// <returns>Session ID</returns>
        public string GetSessionID()
        {
            return this.WebSession.Id;
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
                //result = (T)value;
                result = Utils.GetDeserializedObject<T>(Utils.GetAsString(value));
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


        // Utilities method

        /// <summary>
        /// Get string from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>String for the parameter name</returns>
        public string GetStringFromQuery(string parameterName)
        {
            string result = "";

            // The parameter found
            Parameter foundParameter = this.AjaxParameters
                .Find(parameter => parameter.ParameterName.Equals(parameterName));

            if (Utils.IsNotNull(foundParameter))
            {
                result = Utils.GetAsString(foundParameter.ParameterValue);
            }

            return result;
        }

        /// <summary>
        /// Get integer from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Integer for the parameter name</returns>
        public int GetIntegerFromQuery(string parameterName)
        {
            int result = 0;

            string stringvalue = this.GetStringFromQuery(parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsInteger(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get double from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Double for the parameter name</returns>
        public double GetDoubleFromQuery(string parameterName)
        {
            double result = 0;

            string stringvalue = this.GetStringFromQuery(parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsDouble(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get long from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Long for the parameter name</returns>
        public long GetLongFromQuery(string parameterName)
        {
            long result = 0;

            string stringvalue = this.GetStringFromQuery(parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsLong(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get boolean from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Boolean for the parameter name</returns>
        public bool GetBooleanFromQuery(string parameterName)
        {
            bool result = false;

            string stringvalue = this.GetStringFromQuery(parameterName);

            if (Utils.IsNotNull(stringvalue))
            {
                result = Utils.GetAsBoolean(stringvalue);
            }

            return result;
        }

        /// <summary>
        /// Get generic list from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic list for the parameter name</returns>
        public List<T> GetListFromQuery<T>(string parameterName)
        {
            List<T> resultList = new List<T>();

            string listString = this.GetStringFromQuery(parameterName);

            if (Utils.IsNotEmpty(listString))
            {
                resultList = Utils.GetDeserializedObject<List<T>>(listString);
            }

            return resultList;
        }

        /// <summary>
        /// Get generic dictionary from query
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>Generic dictionary for the parameter name</returns>
        public Dictionary<K,V> GetDictionaryFromQuery<K,V>(string parameterName)
        {
            Dictionary<K,V> resultDictionary = new Dictionary<K,V>();

            string dictionaryString = this.GetStringFromQuery(parameterName);

            if (Utils.IsNotEmpty(dictionaryString))
            {
                resultDictionary = Utils.GetDeserializedObject<Dictionary<K, V>>(dictionaryString);
            }

            return resultDictionary;
        }
    }
}
