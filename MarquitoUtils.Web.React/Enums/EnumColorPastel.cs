using System.Runtime.Serialization;

namespace MarquitoUtils.Web.React.Enums
{
    [DataContract]
    public enum EnumColorPastel
    {
        [EnumMember]
        [EnumColorAttr("None", "FFFFFF00", 0)] None
    }
}
