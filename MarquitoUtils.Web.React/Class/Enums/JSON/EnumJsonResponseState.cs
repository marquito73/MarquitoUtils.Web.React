using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Enums.JSON
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum EnumJsonResponseState
    {
        [EnumMember(Value ="success")]
        Success = 0,
        [EnumMember(Value = "error")]
        Error = 1,
    }
}
