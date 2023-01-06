using MarquitoUtils.Web.React.Class.Components;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Button
{
    public class Button : Component
    {
        public string Caption { get; set; } = "";
        public bool BoldCaption { get; set; } = false;
        public string CaptionColor { get; set; } = "";
        public string BackgroundColor { get; set; } = "";
        public string BorderColor { get; set; } = "";
        public int CaptionSize { get; set; } = 15;
        public string Link { get; set; } = "";
        public Button(string id, string containerId, string caption) : base(id, containerId)
        {
            this.Caption = caption;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
