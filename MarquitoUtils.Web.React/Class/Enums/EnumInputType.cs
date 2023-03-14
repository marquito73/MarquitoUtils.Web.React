using MarquitoUtils.Main.Class.Enums;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumInputType : EnumClass
    {
        public string Type { get; private set; }

        private EnumInputType(string type)
        {
            this.Type = type;
        }

        public static EnumInputType Text
        {
            get
            {
                return new EnumInputType("text");
            }
        }

        public static EnumInputType Password
        {
            get
            {
                return new EnumInputType("password");
            }
        }

        public static EnumInputType Email
        {
            get
            {
                return new EnumInputType("email");
            }
        }

        public static EnumInputType Hidden
        {
            get
            {
                return new EnumInputType("hidden");
            }
        }

        public static EnumInputType Number
        {
            get
            {
                return new EnumInputType("number");
            }
        }

        public static EnumInputType Currency
        {
            get
            {
                return new EnumInputType("number");
            }
        }

        public static EnumInputType Date
        {
            get
            {
                return new EnumInputType("text");
            }
        }

        public static EnumInputType Check
        {
            get
            {
                return new EnumInputType("checkbox");
            }
        }

        public static EnumInputType Toogle
        {
            get
            {
                return new EnumInputType("checkbox");
            }
        }

        public static EnumInputType Radio
        {
            get
            {
                return new EnumInputType("radio");
            }
        }
    }
}
