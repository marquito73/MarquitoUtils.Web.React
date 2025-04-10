namespace MarquitoUtils.Web.React.Class.Components.Chart.SyncFusion
{
    /// <summary>
    /// SyncFusion chart abstract class
    /// </summary>
    /// <typeparam name="TData">The data displayed inside the chart</typeparam>
    public abstract class SyncFusionChart<TData> : Chart<TData>
        where TData : IChartData
    {
        /// <summary>
        /// The SyncFusion license key
        /// </summary>
        public string SyncFusionLicenseKey { get; set; }

        /// <summary>
        /// SyncFusion chart abstract class
        /// </summary>
        /// <param name="id">His ID</param>
        protected SyncFusionChart(string id) : base(id)
        {
        }
    }
}