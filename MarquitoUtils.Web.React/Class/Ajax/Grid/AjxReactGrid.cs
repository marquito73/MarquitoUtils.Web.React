using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.Class.Communication;
using MarquitoUtils.Web.Class.Tools;
using MarquitoUtils.Web.React.Class.Components;
using MarquitoUtils.Web.React.Class.Components.Grid;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Ajax.Grid
{
    public class AjxReactGrid : WebAjax
    {
        private enum GridDataType
        {
            MESSAGE,
            ROWS
        }

        private Dictionary<GridDataType, object> GridData { get; set; }
            = new Dictionary<GridDataType, object>();

        public AjxReactGrid(WebDataEngine webDataEngine) : base(webDataEngine)
        {
        }

        public override IActionResult Exec(string ajaxAction)
        {
            //this.WebDataEngine.SetSessionValue(this.ReactGrid.Id, this.ReactGrid);

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
                            this.GridData.Add(GridDataType.ROWS, reactGrid.getNextRows());
                            break;
                        case "":
                        default:
                            this.GridData.Add(GridDataType.MESSAGE, "Grid action not found");
                            break;
                    }
                    this.WebDataEngine.SetSessionValue(reactGrid.Id, reactGrid);
                }
                else
                {
                    this.GridData.Add(GridDataType.MESSAGE, "Grid not found in Session scope");
                }
            }
            else
            {
                this.GridData.Add(GridDataType.MESSAGE, "Grid id not found in query");
            }

            return GetGridData();
        }

        private IActionResult GetGridData()
        {
            Dictionary<string, object> gridData = new Dictionary<string, object>();

            gridData = this.GridData
                .ToDictionary(data => data.Key.ToString().ToUpper(), data => data.Value);

            return this.GetContentResult(Utils.GetSerializedObject(gridData));
        }
    }
}
