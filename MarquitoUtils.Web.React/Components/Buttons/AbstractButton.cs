using MarquitoUtils.Web.React.Components;

namespace MarquitoUtils.Web.React.Components.Buttons
{
    public abstract class AbstractButton : Component
    {
        public string Caption { get; set; } = "";
        public string Link { get; set; } = "";
        public bool OpenInNewTab { get; set; } = false;
        protected AbstractButton(string id) : base(id)
        {
        }
    }
}
