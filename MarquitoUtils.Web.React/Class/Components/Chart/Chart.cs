using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Chart
{
    public abstract class Chart : Component
    {
        public string LabelColor { get; set; } = "gray";
        public int LabelSize { get; set; } = 20;
        public string ChartTitle { get; set; } = "";
        public string ChartTitleColor { get; set; } = "black";

        protected Chart(string id) : base(id)
        {
        }
    }
}
