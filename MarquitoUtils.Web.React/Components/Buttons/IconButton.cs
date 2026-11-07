using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Components.Buttons
{
    public class IconButton : AbstractButton
    {
        public string IconClass { get; set; } = "";
        public string IconColor { get; set; } = "";
        public int IconSize { get; set; } = 15;

        public IconButton(string id, string iconClass) : base(id)
        {
            this.IconClass = iconClass;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
