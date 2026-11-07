using MarquitoUtils.Main.Common.Enums;

namespace MarquitoUtils.Web.React.Enums
{
    public class EnumSide : EnumClass
    {
        public string Side { get; private set; }

        private EnumSide(string side)
        {
            this.Side = side;
        }

        public static EnumSide Right
        {
            get
            {
                return new EnumSide("right");
            }
        }

        public static EnumSide Left
        {
            get
            {
                return new EnumSide("left");
            }
        }
    }
}
