using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.TextArea
{
    public class TextParagraph : Component
    {
        public string Text { get; set; }
        public string TextColor { get; set; } = "black";
        public int TextSize { get; set; } = 20;
        public string BackgroundColor { get; set; } = "";
        public bool HasViewMoreButton { get; set; } = false;
        public string ViewMoreContent { get; set; } = "";
        public string ViewLessContent { get; set; } = "";
        public string ViewMoreButtonColor { get; set; } = "deepskyblue";

        public TextParagraph(string id, string text) : base(id)
        {
            this.Text = text;
        }
    }
}
