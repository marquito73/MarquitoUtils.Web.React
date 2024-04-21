using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Components.Buttons;
using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.Enums.Action;
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
        /// The element, when clicked, open and load the popup
        /// </summary>
        public string ElementIdForOpenPopup { get; set; }
        /// <summary>
        /// The popup can be resized ?
        /// </summary>
        public bool CanBeResized { get; set; } = false;
        /// <summary>
        /// The popup can be moved ?
        /// </summary>
        public bool CanBeMoved { get; set; } = false;
        public Button? OkButton { get; private set; }
        public string? OkButtonUrl { get; private set; }
        public Button? CancelButton { get; private set; }
        public string? CancelButtonUrl { get; private set; }
        public Button? ValidateButton { get; private set; }
        public string? ValidateButtonUrl { get; private set; }

        /// <summary>
        /// Popup component
        /// </summary>
        /// <param name="id">His id</param>
        /// <param name="popupTitle">Popup title</param>
        /// <param name="elementIdForOpenPopup">Element trigger the opening of the popup</param>
        /// <param name="action">Action to make before load</param>
        public Popup(string id, string popupTitle, string elementIdForOpenPopup, EnumAction action) 
            : base(id)
        {
            this.ReactComponentName = "Popup";
            this.Title = popupTitle;
            this.ElementIdForOpenPopup = elementIdForOpenPopup;
            this.ContentUrl.ActionAction = action.GetEnumName();
        }

        public void AddOkButton(string id, string caption)
        {
            this.OkButton = this.GetPopupActionButton(id, caption);
            this.OkButtonUrl = new WebActionUrl<A>(EnumAction.ClosePopup).GetEncodedUrl();
            this.CancelButton = null;
            this.CancelButtonUrl = null;
            this.ValidateButton = null;
            this.ValidateButtonUrl = null;
        }

        public void AddCancelValidateButtons(string cancelId, string cancelCaption, string validateId, string validateCaption)
        {
            this.OkButton = null;
            this.OkButtonUrl = null;
            this.CancelButton = this.GetPopupActionButton(cancelId, cancelCaption);
            this.CancelButtonUrl = new WebActionUrl<A>(EnumAction.CancelPopup).GetEncodedUrl();
            this.ValidateButton = this.GetPopupActionButton(validateId, validateCaption);
            this.ValidateButtonUrl = new WebActionUrl<A>(EnumAction.ValidatePopup).GetEncodedUrl();
            
        }

        private Button GetPopupActionButton(string id, string caption)
        {
            Button popupActionButton = new Button(id, caption);

            return popupActionButton;
        }
    }
}
