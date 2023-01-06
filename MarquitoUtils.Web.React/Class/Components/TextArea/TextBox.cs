using MarquitoUtils.Web.React.Class.Components;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace MarquitoUtils.Web.React.Class.TextArea
{
    public class TextBox : Component
    {
        public string Value { get; set; }
        public string PlaceHolder { get; set; }
        public TextBox(string id, string containerId) : base(id, containerId)
        {

        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
