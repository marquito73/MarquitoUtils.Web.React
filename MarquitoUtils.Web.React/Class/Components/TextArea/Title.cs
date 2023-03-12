using MarquitoUtils.Web.Class.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.TextArea
{
    internal class Title : Component
    {
        public string Text { get; set; } = "";
        public bool BoldText { get; set; } = false;
        public string TextColor { get; set; } = "";
        public int TextSize { get; set; } = 15;
        public EnumTitleType TitleType { get; set; } = EnumTitleType.H1;

        public Title(string id, string text) : base(id)
        {
            this.Text = text;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
