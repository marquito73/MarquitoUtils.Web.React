using MarquitoUtils.Main.Class.Converters;
using MarquitoUtils.Web.React.Class.Enums.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Communication.JSON
{
    public class JsonResultContent
    {
        public string State { get; set; }
        public object Data { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public JsonResultContent()
        {

        }
    }
}
