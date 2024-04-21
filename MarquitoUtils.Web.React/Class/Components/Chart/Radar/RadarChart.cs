using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Chart.Radar
{
    public class RadarChart<TData> : Component
        where TData : IRadarData
    {
        public IList<TData> Data { get; set; } = new List<TData>();
        public ISet<RadarType> RadarTypes { get; set; } = new HashSet<RadarType>();

        public RadarChart(string id) : base(id)
        {
            this.ReactComponentName = "RadarChart";
        }

        public void AddRadarType(string name, string dataKey, string strokeColor, string fillColor)
        {
            this.RadarTypes.Add(new RadarType()
            {
                Name = name,
                DataKey = dataKey,
                StrokeColor = strokeColor,
                FillColor = fillColor,
            });
        }
    }
}
