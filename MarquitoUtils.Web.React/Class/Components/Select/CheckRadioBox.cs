using MarquitoUtils.Web.React.Class.Components;

namespace MarquitoUtils.Web.React.Class.Select
{
    public abstract class CheckRadioBox : Component
    {
        public string Caption { get; set; } = "";
        public string Value { get; set; } = "";
        public bool Selected { get; set; } = false;
        public string Type { get; private set; } = "checkbox";
        protected CheckRadioBox(string id, string value, bool selected, string type)
            : base(id)
        {
            this.Value = value;
            this.Selected = selected;
            this.Type = type;
        }
    }
}
