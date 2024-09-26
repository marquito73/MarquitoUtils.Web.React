using MarquitoUtils.Web.React.Class.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Select
{
    public class Option : Component
    {
        public string Caption { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
        public enumInputType CheckType { get; set; }

        public Option(string id) : base(id)
        {
        }
    }
}
