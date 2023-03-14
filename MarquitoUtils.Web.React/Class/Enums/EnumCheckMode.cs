using MarquitoUtils.Main.Class.Enums;
using System.Runtime.Serialization;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumCheckModeAttr : EnumClass
    {
        public EnumCheckModeAttr()
        {

        }
    }

    public static class EnumCheckModes
    {
        public static EnumColorAttr Attr(this EnumCheckMode colorType)
        {
            return EnumUtils.GetAttr<EnumColorAttr, EnumCheckMode>(colorType);
        }
    }

    [DataContract]
    public enum EnumCheckMode
    {
        [EnumMember]
        [EnumCheckModeAttr()] Default,
        [EnumMember]
        [EnumCheckModeAttr()] CheckBox,
        [EnumMember]
        [EnumCheckModeAttr()] RadioBox
    }
}
