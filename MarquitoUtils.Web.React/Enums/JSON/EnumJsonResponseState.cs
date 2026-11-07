using System.Runtime.Serialization;

namespace MarquitoUtils.Web.React.Enums.JSON
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum EnumJsonResponseState
    {
        [EnumMember(Value = "success")]
        Success = 0,
        [EnumMember(Value = "error")]
        Error = 1,
    }
}
