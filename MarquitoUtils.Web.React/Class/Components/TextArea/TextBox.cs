using MarquitoUtils.Web.React.Class.Components;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace MarquitoUtils.Web.React.Class.TextArea
{
    public class TextBox : Component
    {
        public string Value { get; set; }
        public string PlaceHolder { get; set; }
        public bool ReadOnly { get; set; } = false;
        public bool SpellCheck { get; set; } = false;
        public string Type { get; set; } = "text";
        public TextBox(string id) : base(id)
        {

        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
