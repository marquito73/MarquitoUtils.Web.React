using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace MarquitoUtils.Web.React.Class.Components.Grid
{
    /// <summary>
    /// A react data grid
    /// </summary>
    /// <typeparam name="Entity">The entity type for load this grid</typeparam>
    public class ReactGrid<Entity> : Component where Entity : class
    {
        /// <summary>
        /// Rows to load each time scroll end
        /// </summary>
        [JsonRequired]
        protected int RowsToLoadEachTime { get; set; } = 20;
        /// <summary>
        /// Use infinite scroll ?
        /// </summary>
        [JsonRequired]
        protected bool UseInfiniteScroll { get; set; } = false;
        /// <summary>
        /// Root url
        /// </summary>
        [JsonRequired]
        private string RootUrl { get; set; }
        /// <summary>
        /// Total of rows
        /// </summary>
        [JsonRequired]
        private int TotalOfRows { get; set; } = 0;
        /// <summary>
        /// Entities
        /// </summary>
        [JsonRequired]
        protected ISet<Entity> Entities { get; } = new HashSet<Entity>();
        /// <summary>
        /// Grid columns
        /// </summary>
        [JsonRequired]
        protected ISet<Column> Columns { get; } = new HashSet<Column>();
        /// <summary>
        /// Grid rows
        /// </summary>
        [JsonRequired]
        protected ISet<Row> Rows { get; } = new HashSet<Row>();
        /// <summary>
        /// Grid loaded rows
        /// </summary>
        [JsonRequired]
        protected ISet<Row> LoadedRows { get; } = new HashSet<Row>();
        /// <summary>
        /// Last loaded rows ids
        /// </summary>
        [JsonRequired]
        protected ISet<int> LastLoadedRows { get; } = new HashSet<int>();
        
        /// <summary>
        /// React grid
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="rootUrl">Root url</param>
        public ReactGrid(string id, string rootUrl) : base(id)
        {
            this.ReactComponentName = "Grid";
            this.RootUrl = rootUrl;

            this.RootUrl = WebUtils.GetCorrectFecthRootUrl(rootUrl);

            this.Rows.Clear();

            this.InitGrid();
            this.InitColumns();
        }

        public override HtmlString GetAsReactJson()
        {
            this.Attributes.Add("cols", this.GetNumberOfColumns());
            this.Attributes.Add("rows", this.GetNumberOfRows());

            this.TotalOfRows = this.LoadedRows.Count;

            return new HtmlString(this.GetInitReactComponent());
        }

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="newEntity">A new entity</param>
        public void AddEntity(Entity newEntity)
        {
            this.Entities.Add(newEntity);
        }

        /// <summary>
        /// Add entities
        /// </summary>
        /// <param name="newEntities">New entities</param>
        public void AddEntities(IEnumerable<Entity> newEntities)
        {
            foreach (Entity newEntity in newEntities)
            {
                this.AddEntity(newEntity);
            }
        }

        /// <summary>
        /// Init grid
        /// </summary>
        protected virtual void InitGrid()
        {

        }

        /// <summary>
        /// Init columns
        /// </summary>
        protected virtual void InitColumns()
        {

        }

        /// <summary>
        /// Load row
        /// </summary>
        /// <param name="entityToLoad">Entity to load</param>
        /// <returns>The row loaded</returns>
        protected virtual Row LoadRow(Entity entityToLoad)
        {
            return null;
        }

        /// <summary>
        /// Init data loading
        /// </summary>
        public void InitLoading()
        {
            foreach (Entity entity in this.Entities)
            {
                Row rowAdded = this.LoadRow(entity);
            }
            if (this.UseInfiniteScroll)
            {
                this.GetNextRows();
            }
        }

        /// <summary>
        /// Load and return next rows
        /// </summary>
        /// <returns>Next rows</returns>
        public ISet<Row> GetNextRows()
        {
            ISet<Row> rows = this.LoadedRows.Where(row => !this.LastLoadedRows.Contains(row.RowNumber))
                .Take(this.RowsToLoadEachTime)
                .ToHashSet();

            foreach (Row row in rows)
            {
                this.LastLoadedRows.Add(row.RowNumber);
                this.Rows.Add(row);
            }

            return rows;
        }

        /// <summary>
        /// Get (and create if not exist) column
        /// </summary>
        /// <param name="colNumber">Column number</param>
        /// <returns>The column found / created</returns>
        protected Column GetColumn(int colNumber)
        {
            ISet<Column> columns = this.Columns
                .Where(col => col.ColNumber.Equals(colNumber))
                .ToHashSet();

            Column newColumn;
            if (Utils.IsEmpty(columns))
            {
                newColumn = new Column(this.Id + "_col_" + colNumber, "", 
                    colNumber, EnumContentType.Text);

                this.Columns.Add(newColumn);
            }
            else
            {
                newColumn = columns.First();
            }

            return newColumn;
        }

        /// <summary>
        /// Get new row
        /// </summary>
        /// <returns>New row</returns>
        protected Row GetNewRow()
        {
            return this.GetRow(this.LoadedRows.Count);
        }

        /// <summary>
        /// Get (and create if not exist) row
        /// </summary>
        /// <param name="rowNumber">Row number</param>
        /// <returns>The row found / created</returns>
        protected Row GetRow(int rowNumber)
        {
            ISet<Row> rows = this.LoadedRows
                .Where(row => row.RowNumber.Equals(rowNumber))
                .ToHashSet();

            Row newRow;
            if (Utils.IsEmpty(rows))
            {
                newRow = new Row(this.Id + "_row_" + rowNumber, rowNumber, this.Columns);

                this.LoadedRows.Add(newRow);

                if (!this.UseInfiniteScroll)
                {
                    this.Rows.Add(newRow);
                }
            }
            else
            {
                newRow = rows.First();
            }

            return newRow;
        }

        /// <summary>
        /// Get number of columns
        /// </summary>
        /// <returns>Number of columns</returns>
        private string GetNumberOfColumns()
        {
            return Utils.GetAsString(this.Columns.Count);
        }

        /// <summary>
        /// Get number of rows
        /// </summary>
        /// <returns>Number of rows</returns>
        private string GetNumberOfRows()
        {
            return Utils.GetAsString(this.Rows.Count);
        }
    }
}
