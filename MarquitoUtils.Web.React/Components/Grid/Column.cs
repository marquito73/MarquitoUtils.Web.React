using MarquitoUtils.Main.Common.Enums;
using MarquitoUtils.Web.React.Components;
using MarquitoUtils.Web.React.Enums;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Components.Grid
{
    public sealed class Column : Component
    {
        public string Caption { get; set; }
        public int ColNumber { get; set; }
        public int ColGroup { get; set; }
        public EnumContentType ColType { get; set; }
        public bool IsEditable { get; set; }
        public EnumCheckMode CheckMode { get; set; }
        public Column(string id, string caption, int colNumber, EnumContentType colType)
            : base(id)
        {
            this.Caption = caption;
            this.ColNumber = colNumber;
            this.ColType = colType;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
