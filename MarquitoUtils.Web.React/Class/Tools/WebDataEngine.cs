using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class WebDataEngine
    {
        //private static string USER_CACHE = "USER_CACHE";

        private readonly string WebSessionMapName = "MAIN_SESSION_MAP";

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
        public DbContext DbContext { get; private set; }
        /// <summary>
        /// The parameters from the query
        /// </summary>
        public List<Parameter> AjaxParameters { get; set; } = new List<Parameter>();
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        public WebDataEngine(ISession webSession, string rootUrl)
        {
            // Set the session
            this.WebSession = webSession;
            // Set the root url of the current session
            this.RootUrl = rootUrl;
            // Init session map if not set
            this.InitSessionMainMap();
        }

        public WebDataEngine(HttpContext webContext) 
            : this(webContext.Session, webContext.Request.Host.Value)
        {
            string test = "";
        }

        public WebDataEngine(ISession webSession, DbContext dbContext, string rootUrl) 
            : this(webSession, rootUrl)
        {
            this.DbContext = dbContext;
        }

        public WebDataEngine(HttpContext webContext, DbContext dbContext) 
            : this(webContext.Session, dbContext, webContext.Request.Host.Value)
        {
            
        }

        public string GetSessionID()
        {
            return this.WebSession.Id;
        }

        private void InitSessionMainMap()
        {
            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            if (Utils.IsNull(sessionMap))
            {
                sessionMap = new Dictionary<string, object>();

                //this.WebSession.Set(this.WebSessionMapName, Utility.ObjectToByteArray(sessionMap));
                
                this.WebSession.SetString(this.WebSessionMapName, Utils.GetSerializedObject(sessionMap));
            }
        }

        private void UpdateSessionMainMap(Dictionary<string, object> sessionMap)
        {
            //this.WebSession.Set(this.WebSessionMapName, Utility.ObjectToByteArray(sessionMap));

            this.WebSession.SetString(this.WebSessionMapName, Utils.GetSerializedObject(sessionMap));
        }

        public Dictionary<string, object> GetSessionMainMap()
        {
            Dictionary<string, object> sessionMap = null;

            byte[] byteValue = this.WebSession.Get(this.WebSessionMapName);

            string sMap = this.WebSession.GetString(this.WebSessionMapName);

            if (Utils.IsNotNull(byteValue))
            {
                //sessionMap = Utility.ByteArrayToObject<Dictionary<string, object>>(byteValue);





                //sessionMap = (Dictionary<string, object>)Convert.ChangeType(byteValue, typeof(Dictionary<string, object>));

                /*using (MemoryStream stream = new MemoryStream(byteValue))
                {
                    sessionMap = System.Text.Json.JsonSerializer
                        .Deserialize<Dictionary<string, object>>(stream.GetBuffer());
                }*/
                sessionMap = Utils.GetDeserializedObject<Dictionary<string, object>>(sMap);
            }

            return sessionMap;
        }

        public T GetSessionValue<T>(string varName)
        {

            object value = null;

            /*byte[] byteValue = this.WebSession.Get(varName);

            if (Utility.IsNotNull(byteValue))
            {
                value = Utility.ByteArrayToObject<T>(byteValue);
            }*/

            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            if (Utils.IsNotEmpty(sessionMap))
            {
                sessionMap.TryGetValue(varName, out value);
                //value = (T) sessionMap.TryGetValue(varName, out value);
            }

            T result = default(T);

            if (Utils.IsNotNull(value))
            {
                //result = (T)value;
                result = Utils.GetDeserializedObject<T>(Utils.GetAsString(value));
            }

            return result;
        }

        public void SetSessionValue(string varName, object var)
        {
            //this.WebSession.Set(varName, Utility.ObjectToByteArray(var));

            Dictionary<string, object> sessionMap = this.GetSessionMainMap();

            if (sessionMap.ContainsKey(varName))
            {
                sessionMap.Remove(varName);
            }
            sessionMap.Add(varName, var);

            this.UpdateSessionMainMap(sessionMap);
        }

        public void RemoveSessionValue(string varName)
        {
            //this.WebSession.Remove(varName);

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
