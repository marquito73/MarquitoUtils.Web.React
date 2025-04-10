namespace MarquitoUtils.Web.React.Class.SideBar
{
    /// <summary>
    /// A menu entry for the side bar
    /// </summary>
    public sealed class MenuEntry
    {
        /// <summary>
        /// The entry key
        /// </summary>
        public string EntryKey { get; set; }
        /// <summary>
        /// An icon to display in the entry's header
        /// </summary>
        public string IconClass { get; set; }
        /// <summary>
        /// A label to display in the entry's header
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// The entry is selected ?
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// The view URL to load
        /// </summary>
        public string ViewURL { get; set; }
        /// <summary>
        /// Sub entries for this entry
        /// </summary>
        public List<MenuEntry> SubEntries { get; set; } = new List<MenuEntry>();
    }
}
