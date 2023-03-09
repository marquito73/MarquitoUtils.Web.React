using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Grid
{
    public class ReactGrid<Entity> : Component where Entity : class
    {
        [JsonRequired]
        protected int RowsToLoadEachTime { get; set; } = 20;
        [JsonRequired]
        protected bool UseInfiniteScroll { get; set; } = false;
        [JsonRequired]
        private string RootUrl { get; set; }
        [JsonRequired]
        private int TotalOfRows { get; set; } = 0;
        [JsonRequired]
        protected ISet<Entity> Entities { get; } = new HashSet<Entity>();
        /*[JsonRequired]
        protected ISet<Entity> LoadedEntities { get; } = new HashSet<Entity>();*/
        [JsonRequired]
        protected ISet<Column> Columns { get; } = new HashSet<Column>();
        [JsonRequired]
        protected ISet<Row> Rows { get; } = new HashSet<Row>();
        //protected ISet<Row> LastLoadedRows { get; } = new HashSet<Row>();
        [JsonRequired]
        protected ISet<Row> LoadedRows { get; } = new HashSet<Row>();
        [JsonRequired]
        protected ISet<int> LastLoadedRows { get; } = new HashSet<int>();
        public ReactGrid(string id, string rootUrl) : base(id)
        {
            this.ReactComponentName = "Grid";
            this.RootUrl = rootUrl;

            this.RootUrl = this.getCorrectFecthRootUrl(rootUrl);

            this.Rows.Clear();

            this.InitGrid();
        }

        private string getCorrectFecthRootUrl(string rootUrl)
        {
            if (rootUrl.Contains("localhost:"))
            {
                if (rootUrl.Contains("/"))
                {
                    rootUrl = rootUrl.Substring(rootUrl.IndexOf("/"));
                }
                else
                {
                    rootUrl = "";
                }
            }

            return rootUrl;
        }

        public override HtmlString GetAsReactJson()
        {
            this.Attributes.Add("cols", this.GetNumberOfColumns());
            this.Attributes.Add("rows", this.GetNumberOfRows());

            this.TotalOfRows = this.LoadedRows.Count;

            return new HtmlString(this.GetInitReactComponent());
        }

        public void addEntity(Entity newEntity)
        {
            this.Entities.Add(newEntity);
        }

        public void addEntities(IEnumerable<Entity> newEntities)
        {
            foreach (Entity newEntity in newEntities)
            {
                this.addEntity(newEntity);
            }
        }

        protected virtual void InitGrid()
        {

        }

        protected virtual Row LoadRow(Entity entityToLoad)
        {
            //return this.LoadRow(entityToLoad);
            return null;
        }

        public void InitLoading()
        {
            /*if (Utils.IsEmpty(this.Rows))
            {
                this.LoadNextRows();
            }*/
            foreach (Entity entity in this.Entities)
            {
                Row rowAdded = this.LoadRow(entity);
            }

            this.getNextRows();
        }

        /*public ISet<Row> getLastLoadedRows()
        {
            return new HashSet<Row>(this.LoadedRows);
        }*/

        public ISet<Row> getNextRows()
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

        /*public void LoadNextRows()
        {
            ISet<Entity> entitiesToLoad = this.Entities
                .Where(entityToLoad => !this.LoadedEntities.Contains(entityToLoad))
                .ToHashSet();

            if (this.UseInfiniteScroll)
            {
                entitiesToLoad = entitiesToLoad.Take(this.RowsToLoadEachTime).ToHashSet();
            }

            this.LoadedEntities.UnionWith(entitiesToLoad);

            foreach (Entity entity in entitiesToLoad)
            {
                Row rowAdded = this.LoadRow(entity);
                this.Rows.Add(rowAdded);
                this.LoadedRows.Add(rowAdded);
            }
        }*/

        //public string Get

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

        protected Row getNewRow()
        {
            return this.GetRow(this.LoadedRows.Count);
        }

        protected Row GetRow(int rowNumber)
        {
            ISet<Row> rows = this.LoadedRows
                .Where(row => row.RowNumber.Equals(rowNumber))
                .ToHashSet();

            Row newRow;
            if (Utils.IsEmpty(rows))
            {
                newRow = new Row(this.Id + "_row_" + rowNumber, rowNumber);

                this.LoadedRows.Add(newRow);
            }
            else
            {
                newRow = rows.First();
            }

            return newRow;
        }

        private string GetNumberOfColumns()
        {
            return Utils.GetAsString(this.Columns.Count);
        }

        private string GetNumberOfRows()
        {
            return Utils.GetAsString(this.Rows.Count);
        }
    }
}
