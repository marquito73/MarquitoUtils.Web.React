using MarquitoUtils.Web.React.Components;
using MarquitoUtils.Web.React.Enums;

namespace MarquitoUtils.Web.React.Components.Select
{
    public class Option : Component
    {
        public string Caption { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
        public enumInputType CheckType { get; set; }

        public Option(string id) : base(id)
        {
        }
    }
}
