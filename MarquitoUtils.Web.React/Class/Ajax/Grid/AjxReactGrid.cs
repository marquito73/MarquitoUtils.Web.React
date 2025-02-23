using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Tools;
using MarquitoUtils.Web.React.Class.Components.Grid;
using Microsoft.AspNetCore.Mvc;
using MarquitoUtils.Main.Class.Logger;

namespace MarquitoUtils.Web.React.Class.Ajax.Grid
{
    /// <summary>
    /// Ajax for a React grid, called for get next rows, manage filters, etc ...
    /// </summary>
    public class AjxReactGrid : WebAjax
    {
        /// <summary>
        /// Data type can be returned to client
        /// </summary>
        private enum GridDataType
        {
            /// <summary>
            /// Message
            /// </summary>
            MESSAGE,
            /// <summary>
            /// New rows
            /// </summary>
            ROWS
        }

        /// <summary>
        /// Dictionnary contain data returned to client
        /// </summary>
        private Dictionary<GridDataType, object> GridData { get; set; }
            = new Dictionary<GridDataType, object>();

        /// <summary>
        /// Ajax for a React grid, called for get next rows, manage filters, etc ...
        /// </summary>
        /// <param name="webDataEngine">The web data engine</param>
        public AjxReactGrid(WebDataEngine webDataEngine) : base(webDataEngine)
        {
        }

        /// <summary>
        /// Execute the ajax grid request
        /// </summary>
        /// <param name="ajaxAction">Ajax grid action</param>
        /// <returns></returns>
        public override IActionResult Exec(string ajaxAction)
        {
            string gridId = this.WebDataEngine.GetStringFromQuery("_gridId");

            if (Utils.IsNotEmpty(gridId))
            {
                ReactGrid<object> reactGrid = this.WebDataEngine
                    .GetSessionValue<ReactGrid<object>>(gridId);
                if (Utils.IsNotNull(reactGrid))
                {
                    // Empty message
                    this.GridData.Add(GridDataType.MESSAGE, "");
                    switch (ajaxAction)
                    {
                        case "getNextRows":
                            this.GridData.Add(GridDataType.ROWS, reactGrid.GetNextRows());
                            Logger.Info("Return grid rows to client");
                            break;
                        case "":
                        default:
                            this.GridData.Add(GridDataType.MESSAGE, "Grid action not found");
                            Logger.Error("Grid action not found");
                            break;
                    }
                    this.WebDataEngine.SetSessionValue(reactGrid.Id, reactGrid);
                }
                else
                {
                    this.GridData.Add(GridDataType.MESSAGE, "Grid not found in Session scope");
                    Logger.Error("Grid not found in Session scope");
                }
            }
            else
            {
                this.GridData.Add(GridDataType.MESSAGE, "Grid id not found in query");
                Logger.Error("Grid id not found in query");
            }

            return GetGridData();
        }

        /// <summary>
        /// Return grid data
        /// </summary>
        /// <returns>Grid data</returns>
        private IActionResult GetGridData()
        {
            Dictionary<string, object> gridData = new Dictionary<string, object>();

            gridData = this.GridData
                .ToDictionary(data => data.Key.ToString().ToUpper(), data => data.Value);

            return this.GetContentResult(Utils.GetSerializedObject(gridData));
        }
    }
}
