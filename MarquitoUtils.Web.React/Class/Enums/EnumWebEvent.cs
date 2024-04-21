using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MarquitoUtils.Main.Class.Enums;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumWebEventAttr : EnumClass
    {
        public string WebEvent { get; private set; }
        public string SimplifiedWebEvent { get; private set; }

        public EnumWebEventAttr(string webEvent) {
            this.WebEvent = "on" + webEvent;
            this.SimplifiedWebEvent = webEvent;
        }

        public string GetEventAsString(string jsFunction)
        {
            StringBuilder sbEvent = new StringBuilder();

            sbEvent.Append(this.WebEvent)
                .Append("=\"")
                .Append(jsFunction)
                .Append("\" ");

            return sbEvent.ToString();
        }
    }

    public static class EnumWebEvents
    {
        public static EnumWebEventAttr Attr(this EnumWebEvent eventType)
        {
            return EnumUtils.GetAttr<EnumWebEventAttr, EnumWebEvent>(eventType);
        }
        public static string GetEventAsString(this EnumWebEvent eventType, string jsFunction)
        {
            return Attr(eventType).GetEventAsString(jsFunction);
        }
    }

    public enum EnumWebEvent
    {
        /// <summary>
        /// Click event
        /// </summary>
        [EnumMember]
        [EnumWebEventAttr("click")] Click,
        [EnumMember]
        [EnumWebEventAttr("mouseenter")] MouseEnter,
        [EnumMember]
        [EnumWebEventAttr("mouseleave")] MouseLeave,
        [EnumMember]
        [EnumWebEventAttr("mousemove")] MouseMove,
        [EnumMember]
        [EnumWebEventAttr("mousedown")] MouseDown,
        [EnumMember]
        [EnumWebEventAttr("mouseup")] MouseUp,
        [EnumMember]
        [EnumWebEventAttr("mouseover")] MouseOver,
        [EnumMember]
        [EnumWebEventAttr("mouseout")] MouseOut,
        [EnumMember]
        [EnumWebEventAttr("touchstart")] TouchStart,
        [EnumMember]
        [EnumWebEventAttr("touchend")] TouchEnd,
        [EnumMember]
        [EnumWebEventAttr("touchmove")] TouchMove,
        [EnumMember]
        [EnumWebEventAttr("check")] Check,
        [EnumMember]
        [EnumWebEventAttr("change")] Change,
        [EnumMember]
        [EnumWebEventAttr("blur")] Blur,
        [EnumMember]
        [EnumWebEventAttr("keyup")] KeyUp,
        [EnumMember]
        [EnumWebEventAttr("keydown")] KeyDown,
        [EnumMember]
        [EnumWebEventAttr("dblclick")] DblClick,
        [EnumMember]
        [EnumWebEventAttr("mouseover")] MouseHover,
        [EnumMember]
        [EnumWebEventAttr("load")] Load,
        [EnumMember]
        [EnumWebEventAttr("focus")] Focus,
        [EnumMember]
        [EnumWebEventAttr("focusin")] FocusIn,
        [EnumMember]
        [EnumWebEventAttr("focusout")] FocusOut,
        [EnumMember]
        [EnumWebEventAttr("submit")] Submit,
        [EnumMember]
        [EnumWebEventAttr("online")] Online,
        [EnumMember]
        [EnumWebEventAttr("offline")] Offline,
        [EnumMember]
        [EnumWebEventAttr("pagehide")] PageHide,
        [EnumMember]
        [EnumWebEventAttr("pageshow")] PageShow,
        [EnumMember]
        [EnumWebEventAttr("drop")] Drop
    }
}
