using MarquitoUtils.Main.Class.Converters;
using Newtonsoft.Json;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Entities
{
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class WebString
    {
        public string Value { get; set; }
        public bool NeedQuotes { get; set; } = true;
        public WebString()
        {

        }

        public WebString(string value)
        {
            this.Value = value;
        }
        public WebString(string value, bool needQuotes)
        {
            this.Value = value;
            this.NeedQuotes = needQuotes;
        }

        public override string ToString()
        {
            StringBuilder sbValue = new StringBuilder();

            if (this.NeedQuotes)
            {
                sbValue.Append("\"").Append(this.Value).Append("\"");
            }
            else
            {
                sbValue.Append(this.Value);
            }

            return sbValue.ToString();
        }
    }
}
