using MarquitoUtils.Main.Class.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Entities
{
    /// <summary>
    /// Object represent a typescript dictionnary (for write the map correctly)
    /// </summary>
    /// <typeparam name="TKey">The key</typeparam>
    /// <typeparam name="TValue">The value</typeparam>
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class WebDictionnary<TKey, TValue> : Dictionary<TKey, TValue>
        where TKey : notnull
    {
        public override string? ToString()
        {
            StringBuilder sbDictionnary = new StringBuilder();

            sbDictionnary.Append("new Map([")
                .Append(string.Join(",", this.Select(this.GetKeyValuePair)))
                .Append("])");

            return sbDictionnary.ToString();
        }

        /// <summary>
        /// Get the key value pair writted correctly
        /// </summary>
        /// <param name="kv">The key value pair</param>
        /// <returns>The key value pair writted correctly</returns>
        private string GetKeyValuePair(KeyValuePair<TKey, TValue> kv)
        {
            object key = kv.Key;
            if (key is Enum)
            {
                key = (int)key;
            }

            return $"[{key},{kv.Value}],";
        }
    }
}
