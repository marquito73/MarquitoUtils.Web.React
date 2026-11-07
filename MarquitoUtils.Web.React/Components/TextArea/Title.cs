using MarquitoUtils.Web.React.Components;
using MarquitoUtils.Web.React.Enums;
using Microsoft.AspNetCore.Html;

namespace MarquitoUtils.Web.React.Components.TextArea
{
    /// <summary>
    /// Title component
    /// </summary>
    public class Title : Component
    {
        /// <summary>
        /// Text of the title
        /// </summary>
        public string Text { get; set; } = "";
        /// <summary>
        /// Use bold text ?
        /// </summary>
        public bool BoldText { get; set; } = true;
        /// <summary>
        /// Text color
        /// </summary>
        public string TextColor { get; set; } = "";
        /// <summary>
        /// Text size
        /// </summary>
        public int TextSize { get; set; } = 15;
        /// <summary>
        /// Title type
        /// </summary>
        public EnumTitleType TitleType { get; set; } = EnumTitleType.H1;

        /// <summary>
        /// Title component
        /// </summary>
        /// <param name="id">His id</param>
        /// <param name="text">His text</param>
        public Title(string id, string text) : base(id)
        {
            this.Text = text;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
