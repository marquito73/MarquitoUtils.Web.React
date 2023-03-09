using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Web.Class.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Grid
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
