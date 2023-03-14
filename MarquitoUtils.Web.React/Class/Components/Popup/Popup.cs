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
    public class Popup<V, A> : Component
        where V : WebView
        where A : WebAction
    {
        public int Width { get; set; } = 300;
        public int Height { get; set; } = 150;
        public string Title { get; set; }
        public WebFunction ClosePopupCallback { get; set; } = new WebFunction();
        public bool ExtendedWhenOpen { get; set; } = false;
        public WebViewUrl<V, A> ContentUrl { get; private set; } = new WebViewUrl<V, A>("");
        public string ElementIdForOpenPopup { get; set; }
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
