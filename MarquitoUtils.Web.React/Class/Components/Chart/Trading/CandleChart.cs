using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Chart.Trading
{
    public class CandleChart : Chart<CandleData>
    {
        public double MaximumPrice { get; set; }
        public double MinimumPrice { get; set; }
        public string StockPriceName { get; set; }
        public string ProductName { get; set; }
        public bool EnableCrossHair { get; set; }
        public int DecimalCount { get; set; }
        public bool UseSignalRForLiveTrading { get; set; }
        public string SignalRHubUrl { get; set; }
        public string SignalRHubMethodName { get; set; }

        public CandleChart(string id) : base(id)
        {
            this.ReactComponentName = "CandleChart";
        }
    }
}
