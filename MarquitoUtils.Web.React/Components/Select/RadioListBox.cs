using MarquitoUtils.Web.React.Enums;

namespace MarquitoUtils.Web.React.Components.Select
{
    public class RadioListBox : ContentBox
    {
        public RadioListBox(string id, string selectedValue) : base(id, selectedValue, enumInputType.Radio)
        {
        }
    }
}
