using MarquitoUtils.Web.React.Class.Components;

namespace MarquitoUtils.Web.React.Class.SideBar
{
    /// <summary>
    /// A side bar component, display a list of menu entries, for load view or execute actions
    /// </summary>
    public class SideBar : Component
    {
        public string Title { get; set; }
        /// <summary>
        /// Entries and sub entries of the side bar
        /// </summary>
        public List<MenuEntry> Entries { get; set; }
        /// <summary>
        /// The width of the side bar
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// The color for the background
        /// </summary>
        public string BackgroundColor { get; set; }
        /// <summary>
        /// The color for entries and sub entries
        /// </summary>
        public string EntryColor { get; set; }
        /// <summary>
        /// The color for selected entries and sub entries
        /// </summary>
        public string EntrySelectedColor { get; set; }
        /// <summary>
        /// The color for hovered entries and sub entries
        /// </summary>
        public string EntryHoverColor { get; set; }
        /// <summary>
        /// The color that outline the side bar
        /// </summary>
        public string OutlineColor { get; set; }
        /// <summary>
        /// The view container, to load when user click on a menu entry
        /// </summary>
        public string ViewContainerID { get; set; }

        /// <summary>
        /// A side bar component, display a list of menu entries, for load view or execute actions
        /// </summary>
        /// <param name="id">His ID</param>
        public SideBar(string id) : base(id)
        {
        }
    }
}
