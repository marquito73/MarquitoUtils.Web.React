using MarquitoUtils.Web.React.Components;
using MarquitoUtils.Web.React.Enums;
using Newtonsoft.Json;

namespace MarquitoUtils.Web.React.Components.Select
{
    public abstract class ContentBox : Component
    {
        public bool IsEditable { get; set; }
        public List<Option> Options { get; private set; } = new List<Option>();
        public string SelectedValue { get; set; }
        public string PlaceHolder { get; set; }
        public bool HasBorder { get; set; } = true;
        public string BackgroundColor { get; set; } = "";
        [JsonIgnore]
        private enumInputType CheckType { get; set; }

        public ContentBox(string id, string selectedValue, enumInputType checkType) : base(id)
        {
            this.SelectedValue = selectedValue;
            this.CheckType = checkType;
        }

        public void AddOption(string caption, string value)
        {
            this.Options.Add(new Option($"{this.Id}_option{value}")
            {
                Name = $"{this.Id}_option",
                Caption = caption,
                Value = value,
                Selected = value == this.SelectedValue,
                CheckType = this.CheckType,
            });
        }
    }
}
