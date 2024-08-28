using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Icon
{
    public class Icon : Component
    {
        public string IconClass { get; set; } = "";
        public string IconColor { get; set; } = "";
        public int IconSize { get; set; } = 15;

        public Icon(string id, string iconClass) : base(id)
        {
            this.IconClass = iconClass;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
