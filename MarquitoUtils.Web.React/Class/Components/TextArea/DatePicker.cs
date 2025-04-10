using Microsoft.AspNetCore.Html;
using static MarquitoUtils.Main.Class.Enums.EnumLang;

namespace MarquitoUtils.Web.React.Class.Components.TextArea
{
    public class DatePicker : Component
    {
        public DateTime Date { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
        public string PlaceHolder { get; set; } = "";
        public LanguageType Language { get; set; } = LanguageType.FR;
        public bool HasBorder { get; set; } = true;
        public string BackgroundColor { get; set; } = "";
        public bool BrightnessWhenHoverFocus { get; set; } = true;
        public DatePicker(string id, DateTime date) : base(id)
        {
            this.Date = date;
        }
    }
}
