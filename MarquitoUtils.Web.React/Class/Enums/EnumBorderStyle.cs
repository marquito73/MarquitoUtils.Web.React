namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumBorderStyle
    {
        public string Style { get; private set; }

        private EnumBorderStyle(string style)
        {
            this.Style = style;
        }

        public static EnumBorderStyle Dashed
        {
            get
            {
                return new EnumBorderStyle("dashed");
            }
        }

        public static EnumBorderStyle Dotted
        {
            get
            {
                return new EnumBorderStyle("dotted");
            }
        }

        public static EnumBorderStyle Double
        {
            get
            {
                return new EnumBorderStyle("double");
            }
        }

        public static EnumBorderStyle Groove
        {
            get
            {
                return new EnumBorderStyle("groove");
            }
        }

        public static EnumBorderStyle Hidden
        {
            get
            {
                return new EnumBorderStyle("hidden");
            }
        }

        public static EnumBorderStyle Inherit
        {
            get
            {
                return new EnumBorderStyle("inherit");
            }
        }

        public static EnumBorderStyle Initial
        {
            get
            {
                return new EnumBorderStyle("initial");
            }
        }

        public static EnumBorderStyle Inset
        {
            get
            {
                return new EnumBorderStyle("inset");
            }
        }

        public static EnumBorderStyle None
        {
            get
            {
                return new EnumBorderStyle("none");
            }
        }

        public static EnumBorderStyle Outset
        {
            get
            {
                return new EnumBorderStyle("outset");
            }
        }

        public static EnumBorderStyle Revert
        {
            get
            {
                return new EnumBorderStyle("revert");
            }
        }

        public static EnumBorderStyle Ridge
        {
            get
            {
                return new EnumBorderStyle("ridge");
            }
        }

        public static EnumBorderStyle Solid
        {
            get
            {
                return new EnumBorderStyle("solid");
            }
        }

        public static EnumBorderStyle Unset
        {
            get
            {
                return new EnumBorderStyle("unset");
            }
        }
    }
}
