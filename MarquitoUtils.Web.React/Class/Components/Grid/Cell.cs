using MarquitoUtils.Main.Class.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Grid
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
