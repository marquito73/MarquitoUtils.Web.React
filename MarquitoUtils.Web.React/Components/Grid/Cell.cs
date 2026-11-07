using MarquitoUtils.Main.Common.Enums;
using MarquitoUtils.Web.React.Components;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Components.Grid
{
    public sealed class Cell : Component
    {
        public object Value { get; set; }
        public bool IsEditable { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }
        public string ColName { get; set; }
        public EnumContentType CellType { get; set; }
        public Cell(string id, int colNumber, int rowNumber, object value)
            : base(id)
        {
            this.ColNumber = colNumber;
            this.RowNumber = rowNumber;
            this.Value = value;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
