using MarquitoUtils.Web.React.Components;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Components.TextArea
{
    public class TextBox : Component
    {
        public string Value { get; set; }
        public string PlaceHolder { get; set; }
        public bool ReadOnly { get; set; } = false;
        public bool SpellCheck { get; set; } = false;
        public string Type { get; set; } = "text";
        public bool HasBorder { get; set; } = true;
        public string BackgroundColor { get; set; } = "";
        public bool BrightnessWhenHoverFocus { get; set; } = true;
        public TextBox(string id) : base(id)
        {

        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
