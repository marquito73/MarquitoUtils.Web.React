using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Common
{
    /// <summary>
    /// Component list of specific components
    /// </summary>
    /// <typeparam name="TComponent">Component's type</typeparam>
    public class ComponentList<TComponent> : Component
        where TComponent : Component
    {
        /// <summary>
        /// Components
        /// </summary>
        public List<TComponent> Components { get; private set; } = new List<TComponent>();

        /// <summary>
        /// Component list of specific components
        /// </summary>
        /// <param name="id">Id of the component's list</param>
        public ComponentList(string id) : base(id)
        {
        }

        public override HtmlString GetAsReactJson()
        {
            StringBuilder sbComponentList = new StringBuilder();

            sbComponentList.Append("<div id='").Append(this.Id).Append("' class='ComponentList-React'").Append(">\n");

            this.Components.ForEach(component =>
            {
                if (sbComponentList.Length > 0)
                {
                    sbComponentList.Append(this.RC);
                }
                sbComponentList.Append(component.GetAsReactJson());
            });

            sbComponentList.Append("</div>");

            return new HtmlString(sbComponentList.ToString());
        }
    }
}
