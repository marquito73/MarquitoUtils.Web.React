using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Web.React.Class.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Spinner
{
    public class Spinner : Component
    {
        public string SpinnerIcon { get; set; }
        public string SpinnerIconColor { get; set; }

        public Spinner(string id, EnumIcon icon, string color = "black") : base(id)
        {
            this.SpinnerIcon = icon.Attr().IconCss;
            this.SpinnerIconColor = color;

            this.CssClass.Add("hidden");
        }
    }
}