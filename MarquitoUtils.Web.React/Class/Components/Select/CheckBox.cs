using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Class.Select
{
    public class CheckBox : CheckRadioBox
    {
        public CheckBox(string id, string containerId, string value, bool selected, string caption) 
            : base(id, containerId, value, selected, "checkbox")
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
