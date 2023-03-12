using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.TextArea
{
    public class Label : Component
    {
        public string Text { get; set; } = "";
        public string For { get; set; } = "";
        public bool BoldText { get; set; } = false;
        public string TextColor { get; set; } = "";
        public int TextSize { get; set; } = 12;
        public Label(string id, string text) : base(id)
        {
            this.Text = text;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
