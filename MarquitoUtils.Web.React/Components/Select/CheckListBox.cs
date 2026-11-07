using MarquitoUtils.Web.React.Enums;

namespace MarquitoUtils.Web.React.Components.Select
{
    public class CheckListBox : ContentBox
    {
        public CheckListBox(string id, string selectedValue) : base(id, selectedValue, enumInputType.Check)
        {
        }
    }
}
