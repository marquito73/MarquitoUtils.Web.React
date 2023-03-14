using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Enums
{
    [DataContract]
    public enum EnumColorPastel
    {
        [EnumMember]
        [EnumColorAttr("None", "FFFFFF00", 0)] None
    }
}
