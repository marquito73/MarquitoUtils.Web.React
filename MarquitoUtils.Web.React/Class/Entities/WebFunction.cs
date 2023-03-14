using MarquitoUtils.Main.Class.Converters;
using Newtonsoft.Json;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Entities
{
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class WebFunction
    {
        public string Function { get; set; } = "() => {}";

        public WebFunction()
        {

        }

        public WebFunction(string function)
        {
            this.Function = function;
        }

        public override string ToString()
        {
            StringBuilder sbFunction = new StringBuilder();

            return new WebString(this.Function, false).ToString();
        }
    }
}
