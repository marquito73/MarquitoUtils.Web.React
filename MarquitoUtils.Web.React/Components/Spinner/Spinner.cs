using MarquitoUtils.Web.React.Enums;

namespace MarquitoUtils.Web.React.Components.Spinner
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