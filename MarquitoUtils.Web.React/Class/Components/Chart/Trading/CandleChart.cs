namespace MarquitoUtils.Web.React.Class.Components.Chart.Trading
{
    /// <summary>
    /// A chart with candles
    /// </summary>
    public class CandleChart : Chart<CandleData>
    {
        /// <summary>
        /// The maximum price
        /// </summary>
        public double MaximumPrice { get; set; }
        /// <summary>
        /// The minimum price
        /// </summary>
        public double MinimumPrice { get; set; }
        /// <summary>
        /// The stock price name
        /// </summary>
        public string StockPriceName { get; set; }
        /// <summary>
        /// The product name
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Enable zoom and pan ?
        /// </summary>
        public bool EnableCrossHair { get; set; }
        /// <summary>
        /// Number of decimals
        /// </summary>
        public int DecimalCount { get; set; }
        /// <summary>
        /// Use signalR Hub for live trading ?
        /// </summary>
        public bool UseSignalRForLiveTrading { get; set; }
        /// <summary>
        /// SignalR hub URL
        /// </summary>
        public string SignalRHubUrl { get; set; }
        /// <summary>
        /// SignalR hub method name
        /// </summary>
        public string SignalRHubMethodName { get; set; }
        /// <summary>
        /// Period (in seconds)
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// A chart with candles
        /// </summary>
        /// <param name="id"></param>
        public CandleChart(string id) : base(id)
        {
            this.ReactComponentName = "CandleChart";
        }
    }
}
