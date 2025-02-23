using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Chart.Radar
{
    public interface IRadarData : IChartData
    {
        public string Subject { get; set; }
    }
}
