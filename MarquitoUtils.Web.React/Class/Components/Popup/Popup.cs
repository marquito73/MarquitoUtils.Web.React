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
        public WebViewUrl<V, A> ContentUrl { get; private set; } = new WebViewUrl<V, A>(EnumAction.LoadView);
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
        /// <summary>
        /// The main color used for style this popup
        /// </summary>
        public string MainStyleColor { get; set; } = "deepskyblue";
        /// <summary>
        /// Main background color
        /// </summary>
        public string MainBackgroundColor {  get; set; }
        /// <summary>
        /// Iframe background color
        /// </summary>
        public string IframeBackgroundColor { get; set; }
        /// <summary>
        /// The ok button
        /// </summary>
        public Button? OkButton { get; private set; }
        /// <summary>
        /// The ok button url
        /// </summary>
        public string? OkButtonUrl { get; private set; }
        /// <summary>
        /// The cancel button
        /// </summary>
        public Button? CancelButton { get; private set; }
        /// <summary>
        /// The cancel button url
        /// </summary>
        public string? CancelButtonUrl { get; private set; }
        /// <summary>
        /// The validate button
        /// </summary>
        public Button? ValidateButton { get; private set; }
        /// <summary>
        /// The validate button url
        /// </summary>
        public string? ValidateButtonUrl { get; private set; }
        /// <summary>
        /// The close URL
        /// </summary>
        public string? CloseUrl { get; private set; }

        /// <summary>
        /// Popup component
        /// </summary>
        /// <param name="id">His id</param>
        /// <param name="popupTitle">Popup title</param>
        /// <param name="action">Action to make before load</param>
        public Popup(string id, string popupTitle, EnumAction action) 
            : base(id)
        {
            this.ReactComponentName = "Popup";
            this.Title = popupTitle;
            this.ContentUrl.ActionAction = action;
            this.CloseUrl = new WebActionUrl<A>(EnumAction.ClosePopup).GetEncodedUrl();
        }

        /// <summary>
        /// Popup component
        /// </summary>
        /// <param name="id">His id</param>
        /// <param name="popupTitle">Popup title</param>
        /// <param name="action">Action to make before load</param>
        /// <param name="elementIdForOpenPopup">Element trigger the opening of the popup</param>
        public Popup(string id, string popupTitle, EnumAction action, string elementIdForOpenPopup)
            : this(id, popupTitle, action)
        {
            this.ElementIdForOpenPopup = elementIdForOpenPopup;
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
            Button popupActionButton = new Button(id, caption)
            {
                BorderColor = "rgba(255,255,255,0.9)"
            };

            return popupActionButton;
        }
    }
}
