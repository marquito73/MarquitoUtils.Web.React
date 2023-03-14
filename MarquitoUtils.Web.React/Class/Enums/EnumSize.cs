using MarquitoUtils.Main.Class.Enums;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumSize : EnumClass
    {
        public string Size { get; private set; }

        private EnumSize(string size)
        {
            this.Size = size;
        }

        public static EnumSize GetSizeByName(string sizeName)
        {
            //return GetEnumByName<EnumSize>(sizeName);
            // TODO A refaire
            return new EnumSize(sizeName);
        }

        public static List<EnumSize> GetSizeList()
        {
            //return GetEnumList<EnumSize>();
            return new List<EnumSize>();
        }

        public static EnumSize VerySmall
        {
            get
            {
                return new EnumSize("very_small");
            }
        }

        public static EnumSize Small
        {
            get
            {
                return new EnumSize("small");
            }
        }

        public static EnumSize Medium
        {
            get
            {
                return new EnumSize("medium");
            }
        }

        public static EnumSize Big
        {
            get
            {
                return new EnumSize("big");
            }
        }

        public static EnumSize VeryBig
        {
            get
            {
                return new EnumSize("very_big");
            }
        }

        public static EnumSize FullSize
        {
            get
            {
                return new EnumSize("full_size");
            }
        }
    }
}
