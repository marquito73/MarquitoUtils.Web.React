using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Select
{
    public class RadioBox : CheckRadioBox
    {
        public RadioBox(string id, string value, bool selected, string caption)
            : base(id, value, selected, "radiobox")
        {
            this.CssClass.Add("radioBox");
            this.Caption = caption;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
