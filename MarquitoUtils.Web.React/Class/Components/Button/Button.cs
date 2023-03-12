using MarquitoUtils.Web.React.Class.Components;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Components.Button
{
    public class Button : AbstractButton
    {
        public bool BoldCaption { get; set; } = false;
        public string CaptionColor { get; set; } = "";
        public string BackgroundColor { get; set; } = "";
        public string BorderColor { get; set; } = "";
        public int CaptionSize { get; set; } = 15;
        public Button(string id, string caption) : base(id)
        {
            this.Caption = caption;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
