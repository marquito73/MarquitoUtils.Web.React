using MarquitoUtils.Main.Class.Converters;
using Newtonsoft.Json;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Entities
{
    /// <summary>
    /// Object represent a string in javascript
    /// </summary>
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class WebString
    {
        /// <summary>
        /// The value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Need quotes ? (true for pass the string as value, false for pass the string as variable name)
        /// </summary>
        public bool NeedQuotes { get; set; }
        /// <summary>
        /// Quote type (" ')
        /// </summary>
        public char QuoteType { get; set; }

        /// <summary>
        /// Object represent a string in javascript
        /// </summary>
        public WebString()
        {

        }

        /// <summary>
        /// Object represent a string in javascript
        /// </summary>
        /// <param name="value">The string value</param>
        /// <param name="needQuotes">Need quotes ?</param>
        /// <param name="quoteType">The quote type</param>
        public WebString(string value, bool needQuotes = true, char quoteType = '\"')
        {
            this.Value = value;
            this.NeedQuotes = needQuotes;
            this.QuoteType = quoteType;
        }

        public override string ToString()
        {
            StringBuilder sbValue = new StringBuilder();

            if (this.NeedQuotes)
            {
                sbValue.Append(this.QuoteType).Append(this.Value).Append(this.QuoteType);
            }
            else
            {
                sbValue.Append(this.Value);
            }

            return sbValue.ToString();
        }
    }
}
