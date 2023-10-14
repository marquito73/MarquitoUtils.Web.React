using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.Tools;
using MarquitoUtils.Web.React.Class.Url;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Components
{
    /// <summary>
    /// React main component
    /// </summary>
    public abstract class Component
    {
        #region Components properties

        /// <summary>
        /// It's just a carriage return
        /// </summary>
        protected readonly string RC = "\n";
        /// <summary>
        /// It's just a tab
        /// </summary>
        protected readonly string TAB = "\t";
        protected string ReactComponentName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// A list of css class of the component
        /// </summary>
        public List<string> CssClass { get; set; } = new List<string>();
        /// <summary>
        /// A dictionary of attributes styles, with attribute's key for Key, attribute'svalue for Value
        /// </summary>
        public WebDictionnary<string, string> Attributes { get; set; } = new WebDictionnary<string, string>();
        /// <summary>
        /// A dictionary of events, with event's enum for Key, event's js function for Value
        /// </summary>
        public WebDictionnary<EnumWebEvent, WebFunction> Events { get; set; }
            = new WebDictionnary<EnumWebEvent, WebFunction>();

        #endregion Components properties

        /// <summary>
        /// React component base class
        /// </summary>
        /// <param name="id"></param>
        public Component(string id)
        {
            this.Id = id;
            this.Name = id;
            this.ReactComponentName = this.GetType().Name;
        }

        /// <summary>
        /// Get react component as script contain json data to invoke react component
        /// </summary>
        /// <returns>React component as script contain json data to invoke react component</returns>
        public virtual HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }


        protected string GetStringInsideReactScript(string script)
        {
            StringBuilder sbScript = new StringBuilder();

            //https://www.w3schools.com/tags/ev_onload.asp
            sbScript.Append("<div id=\"").Append(this.Id).Append("_container\">").Append(this.RC)
                .Append("<script>").Append(this.RC)
                .Append("window.addEventListener(\"DOMContentLoaded\", () => {").Append(this.RC)
                .Append(script).Append(this.RC)
                .Append("}, false);").Append(this.RC)
                .Append("</script>").Append(this.RC)
                .Append("</div>");

            return sbScript.ToString();
        }

        protected string GetInitReactComponent()
        {
            StringBuilder sbJson = new StringBuilder();

            string componentProps = WebUtils.GetSerializedObject(this)
                .Replace("{}", "new Map()")
                .Replace("[]", "new Array()");

            sbJson.Append("window").Append(".").Append("ReactWidgetFactory").Append(".")
                .Append("create").Append(this.ReactComponentName).Append("(")
                .Append(componentProps).Append(", \"").Append(this.Id).Append("_container\"")
                .Append(");");

            return this.GetStringInsideReactScript(sbJson.ToString());
        }

        /// <summary>
        /// Add an Ajax event
        /// </summary>
        /// <typeparam name="TUrl">The url for retrieve the Ajax / Action</typeparam>
        /// <param name="webEvent">The event type</param>
        /// <param name="url">The url</param>
        /// <param name="jsFunction">The function to execute on the front when event is triggered</param>
        public void AddAjaxEvent<TUrl>(EnumWebEvent webEvent, TUrl url, string jsFunction = "")
            where TUrl : WebUrl
        {
            StringBuilder sbAjaxEvent = new StringBuilder();

            sbAjaxEvent.Append("window").Append(".").Append("ReactWidgetFactory").Append(".")
                .Append("AjaxUtils").Append(".").Append("PostDataWithUrl").Append("(")
                .Append(url.GetEncodedUrl()).Append(")");

            if (this.Events.ContainsKey(webEvent))
            {
                this.Events[webEvent] = new WebFunction(sbAjaxEvent.ToString());
            }
            else
            {
                this.Events.Add(webEvent, new WebFunction(sbAjaxEvent.ToString()));
            }
        }
    }
}
