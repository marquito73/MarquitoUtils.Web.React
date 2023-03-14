using MarquitoUtils.Main.Class.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumUrlType : EnumClass
    {
        public string UrlType { get; private set; }

        private EnumUrlType(string urlType)
        {
            this.UrlType = urlType;
        }

        public static EnumUrlType Action = new EnumUrlType("action");
        public static EnumUrlType Ajax = new EnumUrlType("ajax");
        public static EnumUrlType View = new EnumUrlType("view");
        public static EnumUrlType Fragment = new EnumUrlType("fragment");
    }
}
