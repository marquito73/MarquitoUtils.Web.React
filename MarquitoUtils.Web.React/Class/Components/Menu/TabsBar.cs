using MarquitoUtils.Web.Class.Entities;
using MarquitoUtils.Web.React.Class.Components;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Menu
{
    public class TabsBar : Component
    {
        public List<Tab> Tabs { get; set; } = new List<Tab>();
        private int NextTabNumber { get; set; } = 0;
        public TabsBar(string id, string containerId) : base(id, containerId)
        {
            this.ReactComponentName = "Tabs";
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }

        public void AddTab(string caption, WebFunction onClickFunction)
        {
            Tab newTab = new Tab(this.Id + this.NextTabNumber, "", caption, onClickFunction);
            this.Tabs.Add(newTab);

            this.NextTabNumber++;
        }
    }
}
