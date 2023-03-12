using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Grid
{
    public sealed class Row : Component, IComparable<Row>
    {
        public int RowNumber { get; set; }
        [JsonRequired]
        private ISet<Cell> Cells { get; set; } = new HashSet<Cell>();
        [JsonRequired]
        private ISet<Column> Columns { get; set; } = new HashSet<Column>();
        public Row(string id, int rowNumber, ISet<Column> columns) : base(id)
        {
            this.RowNumber = rowNumber;
            this.Columns = columns;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }

        public Cell GetCell(int colNumber)
        {
            ISet<Cell> cells = this.Cells
                .Where(cell => cell.ColNumber.Equals(colNumber))
                .Where(cell => cell.RowNumber.Equals(this.RowNumber))
                .ToHashSet();

            Cell newCell;
            if (Utils.IsEmpty(cells))
            {

                StringBuilder sbCellName = new StringBuilder();

                sbCellName.Append(this.Id).Append("_").Append("cell").Append("_")
                    .Append(colNumber).Append("_").Append(this.RowNumber);

                newCell = new Cell(sbCellName.ToString(), colNumber, this.RowNumber, "");

                Column column = this.Columns
                    .Where(col => col.ColNumber.Equals(colNumber)).First();
                newCell.CellType = column.ColType;
                newCell.IsEditable = column.IsEditable;
                newCell.ColName = column.Name;

                this.Cells.Add(newCell);
            }
            else
            {
                newCell = cells.First();
            }

            return newCell;
        }

        public int CompareTo(Row? otherRow)
        {
            if (Utils.IsNotNull(otherRow))
            {
                return this.RowNumber.CompareTo(otherRow.RowNumber);
            }
            else
            {
                return 0;
            }
        }
    }
}
