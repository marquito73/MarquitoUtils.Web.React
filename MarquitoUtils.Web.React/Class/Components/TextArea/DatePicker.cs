using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarquitoUtils.Main.Class.Enums.EnumLang;

namespace MarquitoUtils.Web.React.Class.Components.TextArea
{
    public class DatePicker : Component
    {
        public DateTime Date { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
        public string PlaceHolder { get; set; } = "";
        public enumLang Language { get; set; } = enumLang.FR;
        public DatePicker(string id, DateTime date) : base(id)
        {
            this.Date = date;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
