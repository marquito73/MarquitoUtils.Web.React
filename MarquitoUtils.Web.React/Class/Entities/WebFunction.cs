using MarquitoUtils.Main.Class.Converters;
using MarquitoUtils.Main.Class.Tools;
using Newtonsoft.Json;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Entities
{
    /// <summary>
    /// Object represent a javascript function
    /// </summary>
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class WebFunction
    {
        public string Function { get; set; } = "() => {}";
        public ISet<string> ParametersNames { get; private set; } = new HashSet<string>();
        public List<object> ParametersValues { get; private set; } = new List<object>();

        public WebFunction()
        {

        }

        /// <summary>
        /// Object represent a javascript function
        /// </summary>
        /// <param name="function">The function name</param>
        public WebFunction(string function) : this()
        {
            this.Function = function;
        }

        /// <summary>
        /// Object represent a javascript function
        /// </summary>
        /// <param name="function">The function name</param>
        /// <param name="parametersNames">The function parameter's names</param>
        public WebFunction(string function, params string[] parametersNames) : this(function)
        {
            parametersNames.ToList().ForEach(param => this.ParametersNames.Add(param));
        }

        public override string ToString()
        {
            StringBuilder sbFunction = new StringBuilder();

            sbFunction.Append($"({string.Join(",", this.ParametersNames)}) => ")
                .Append(this.Function).Append("(");

            bool addSeparator = false;
            foreach (object parameter in this.ParametersValues)
            {
                if (addSeparator)
                {
                    sbFunction.Append(",");
                }

                if (parameter is string)
                {
                    sbFunction.Append(new WebString(Utils.GetAsString(parameter), true, '\''));
                } 
                else
                {
                    sbFunction.Append(Utils.GetAsString(parameter));
                }

                addSeparator = true;
            }

            sbFunction.Append(")");

            return new WebString(sbFunction.ToString(), false).ToString();
        }
    }
}
