using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.Tools;

namespace MarquitoUtils.Web.React.Class.Components.Select.Flag
{
    /// <summary>
    /// A component for choose a language / country
    /// </summary>
    public class CountryFlagListBox : Component
    {
        /// <summary>
        /// Countries to display, if the list is empty, all countries flags are displayed
        /// </summary>
        public List<string> FilterCountries { get; private set; } = new List<string>();
        /// <summary>
        /// The selected flag
        /// </summary>
        public string SelectedFlag { get; set; }

        /// <summary>
        /// A component for choose a language / country
        /// </summary>
        /// <param name="id">The id of the country flag list box</param>
        public CountryFlagListBox(string id) : base(id)
        {
        }

        /// <summary>
        /// Add an event for change the server language when the user change the flag
        /// </summary>
        /// <param name="rootUrl">The root url of the server</param>
        public void AddChangeLanguageEvent(string rootUrl)
        {
            WebFunction ajaxFunction = new WebFunction("window.ReactWidgetFactory.AjaxUtils().constructor.ChangeLanguage", 
                "props", "state");

            ajaxFunction.ParametersValues.Add(WebUtils.GetCorrectFecthRootUrl(rootUrl));
            ajaxFunction.ParametersValues.Add(new WebString("state.SelectedFlagValue", false));

            if (this.Events.ContainsKey(EnumWebEvent.Change))
            {
                this.Events[EnumWebEvent.Change] = ajaxFunction;
            }
            else
            {
                this.Events.Add(EnumWebEvent.Change, ajaxFunction);
            }
        }
    }
}
