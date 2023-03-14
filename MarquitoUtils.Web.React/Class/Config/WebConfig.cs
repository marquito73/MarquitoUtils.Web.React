using MarquitoUtils.Web.React.Class.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Config
{
    public class WebConfig
    {
        public EnumColor DefaultBackgroundColor { get; set; } = EnumColor.White;
        public EnumColor DefaultBorderColor { get; set; } = EnumColor.Black;
        public string DefaultBorderStyle { get; set; } = "";
        public EnumColor DefaultFontColor { get; set; } = EnumColor.Black;
        public EnumSize DefaultFontSize { get; set; } = EnumSize.Medium;

        public WebConfig()
        {

        }
    }
}
