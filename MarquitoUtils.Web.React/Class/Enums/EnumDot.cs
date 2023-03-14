using MarquitoUtils.Main.Class.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumDot : EnumClass
    {
        public string DotClass { get; private set; } = "";

        private EnumDot(string dotClass)
        {
            this.DotClass = dotClass;
        }

        public static EnumDot FILLED
        {
            get
            {
                return new EnumDot("icon-dot");
            }
        }

        public static EnumDot HALF
        {
            get
            {
                return new EnumDot("icon-half-dot");
            }
        }

        public static EnumDot EMPTY
        {
            get
            {
                return new EnumDot("icon-empty-dot");
            }
        }
    }
}
