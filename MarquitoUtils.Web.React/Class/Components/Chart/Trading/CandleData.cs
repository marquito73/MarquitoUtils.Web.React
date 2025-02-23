using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Chart.Trading
{
    [DebuggerDisplay("High = {High}, Low = {Low}, Open = {Open}, Close = {Close}, Volume = {Volume}, Time = {Time}")]
    public class CandleData : IChartData
    {
        public DateTime Time { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }

        public double GetMinValue()
        {
            return new[] 
            { 
                this.High,
                this.Low,
                this.Open,
                this.Close,
            }.Min();
        }

        public double GetMaxValue()
        {
            return new[]
            {
                this.High,
                this.Low,
                this.Open,
                this.Close,
            }.Max();
        }
    }
}
