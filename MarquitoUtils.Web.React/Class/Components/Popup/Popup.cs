using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Url;
using MarquitoUtils.Web.React.Class.Views;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Popup
{
    /// <summary>
    /// Popup component
    /// </summary>
    /// <typeparam name="V">The web view inside the popup</typeparam>
    /// <typeparam name="A">The action executed before load the popup</typeparam>
    public class Popup<V, A> : Component
        where V : WebView
        where A : WebAction
    {
        /// <summary>
        /// Popup width
        /// </summary>
        public int Width { get; set; } = 300;
        /// <summary>
        /// Popup height
        /// </summary>
        public int Height { get; set; } = 150;
        /// <summary>
        /// Popup title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Popup close callback function
        /// </summary>
        public WebFunction ClosePopupCallback { get; set; } = new WebFunction();
        /// <summary>
        /// Popup is extended when opened ?
        /// </summary>
        public bool ExtendedWhenOpen { get; set; } = false;
        /// <summary>
        /// The url to load the popup
        /// </summary>
        public WebViewUrl<V, A> ContentUrl { get; private set; } = new WebViewUrl<V, A>("");
        /// <summary>
        /// Need to reload the view each time we open the popup ?
        /// </summary>
        public bool ReloadEachTimeOpened { get; set; } = false;
        /// <summary>
        /// Element trigger the opening of the popup
        /// </summary>
        public string ElementIdForOpenPopup { get; set; }

        /// <summary>
        /// Popup component
        /// </summary>
        /// <param name="id">His id</param>
        /// <param name="popupTitle">Popup title</param>
        /// <param name="elementIdForOpenPopup">Element trigger the opening of the popup</param>
        /// <param name="action">Action to make before load</param>
        public Popup(string id, string popupTitle, string elementIdForOpenPopup, string action) 
            : base(id)
        {
            this.ReactComponentName = "Popup";
            this.Title = popupTitle;
            this.ElementIdForOpenPopup = elementIdForOpenPopup;
            this.ContentUrl.ActionAction = action;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
