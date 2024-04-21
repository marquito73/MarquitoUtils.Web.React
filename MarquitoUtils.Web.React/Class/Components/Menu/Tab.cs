using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Components.Buttons;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Components.Menu
{
    /// <summary>
    /// React tab
    /// </summary>
    public class Tab : Button
    {
        /// <summary>
        /// Javascript function executed on tab's click
        /// </summary>
        public WebFunction OnClick { get; set; } = new WebFunction();

        /// <summary>
        /// Tab
        /// </summary>
        /// <param name="id">Id of tab</param>
        /// <param name="caption">His caption</param>
        /// <param name="onClickFunction">Javascript function executed on tab's click</param>
        public Tab(string id, string caption, WebFunction onClickFunction) 
            : base(id, caption)
        {
            this.OnClick = onClickFunction;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
