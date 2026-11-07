using MarquitoUtils.Web.React.Components;

namespace MarquitoUtils.Web.React.Components.Chart
{
    public abstract class Chart<TData> : Component
        where TData : IChartData
    {
        public IList<TData> Data { get; set; } = new List<TData>();
        public string LabelColor { get; set; } = "gray";
        public int LabelSize { get; set; } = 20;
        public string ChartTitle { get; set; } = "";
        public string ChartTitleColor { get; set; } = "black";
        public string? BackgroundColor { get; set; }

        protected Chart(string id) : base(id)
        {
        }
    }
}
