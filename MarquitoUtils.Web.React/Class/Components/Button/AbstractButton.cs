using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Button
{
    public abstract class AbstractButton : Component
    {
        public string Caption { get; set; } = "";
        public string Link { get; set; } = "";
        public bool OpenInNewTab { get; set; } = false;
        protected AbstractButton(string id) : base(id)
        {
        }
    }
}
