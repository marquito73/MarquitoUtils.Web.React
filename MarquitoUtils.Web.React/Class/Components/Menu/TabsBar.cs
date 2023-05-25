using MarquitoUtils.Web.React.Class.Entities;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Components.Menu
{
    /// <summary>
    /// Tabs bar
    /// </summary>
    public class TabsBar : Component
    {
        /// <summary>
        /// List of tabs
        /// </summary>
        public List<Tab> Tabs { get; set; } = new List<Tab>();
        /// <summary>
        /// The next tab number
        /// </summary>
        private int NextTabNumber { get; set; } = 0;

        /// <summary>
        /// The tabs bar
        /// </summary>
        /// <param name="id">His id</param>
        public TabsBar(string id) : base(id)
        {
            this.ReactComponentName = "Tabs";
        }

        /// <summary>
        /// Add new tab
        /// </summary>
        /// <param name="caption">The tab caption</param>
        /// <param name="onClickFunction">The tab's click function</param>
        /// <param name="link">The link for redirection</param>
        public void AddTab(string caption, WebFunction onClickFunction, string link = "")
        {
            Tab newTab = new Tab(this.Id + this.NextTabNumber, caption, onClickFunction);
            newTab.Link = link;
            this.Tabs.Add(newTab);

            this.NextTabNumber++;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
