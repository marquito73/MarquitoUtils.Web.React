using MarquitoUtils.Web.Class.Entities;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Menu
{
    public class Tab : Button.Button
    {
        public WebFunction OnClick { get; set; } = new WebFunction();
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
