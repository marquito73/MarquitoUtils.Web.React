using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Select
{
    public class CheckBox : CheckRadioBox
    {
        public CheckBox(string id, string value, bool selected, string caption) 
            : base(id, value, selected, "checkbox")
        {
            this.CssClass.Add("checkBox");
            this.Caption = caption;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
