using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.TextArea
{
    public class TextArea : Component
    {
        public string Value { get; set; } = "";
        public string PlaceHolder { get; set; } = "";
        public bool ReadOnly { get; set; } = false;
        public bool SpellCheck { get; set; } = false;
        public bool CanHorizontallyResize { get; set; } = false;
        public bool CanVerticalResize { get; set; } = false;
        public TextArea(string id, string value) : base(id)
        {
            this.Value = value;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
