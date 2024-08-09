using MarquitoUtils.Web.React.Class.Communication;
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

            sbScript.Append($"<div id=\"{this.Id}_container\" class=\"{this.ReactComponentName}-React-Container\">").Append(this.RC)
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
            string componentProps = WebUtils.GetSerializedObject(this)
                .Replace("{}", "new Map()")
                .Replace("[]", "new Array()");

            string json = $"window.ReactWidgetFactory.create{this.ReactComponentName}({componentProps}, \"{this.Id}_container\");";

            return this.GetStringInsideReactScript(json);
        }

        /// <summary>
        /// Add an Ajax event
        /// </summary>
        /// <typeparam name="TUrl">The url for retrieve the Ajax</typeparam>
        /// <param name="webEvent">The event type</param>
        /// <param name="url">The url</param>
        /// <param name="jsFunction">The function to execute on the front when event is triggered</param>
        public void AddAjaxEvent<TUrl, TAjax>(EnumWebEvent webEvent, TUrl url, string jsFunction = "")
            where TUrl : WebAjaxUrl<TAjax>
            where TAjax : WebAjax
        {
            string ajaxEvent = $"window.ReactWidgetFactory.AjaxUtils().PostDataWithUrl({url})";

            if (this.Events.ContainsKey(webEvent))
            {
                this.Events[webEvent] = new WebFunction(ajaxEvent);
            }
            else
            {
                this.Events.Add(webEvent, new WebFunction(ajaxEvent));
            }
        }

        /// <summary>
        /// Add an Action event
        /// </summary>
        /// <typeparam name="TUrl">The url for retrieve the Action</typeparam>
        /// <param name="webEvent">The event type</param>
        /// <param name="url">The url</param>
        /// <param name="jsFunction">The function to execute on the front when event is triggered</param>
        public void AddActionEvent<TUrl, TAction>(EnumWebEvent webEvent, TUrl url, string jsFunction = "")
            where TUrl : WebActionUrl<TAction>
            where TAction : WebAction
        {
            string actionEvent = $"window.ReactWidgetFactory.AjaxUtils().PostDataWithUrl({url})";

            if (this.Events.ContainsKey(webEvent))
            {
                this.Events[webEvent] = new WebFunction(actionEvent);
            }
            else
            {
                this.Events.Add(webEvent, new WebFunction(actionEvent));
            }
        }
    }
}
