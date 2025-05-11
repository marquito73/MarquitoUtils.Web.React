using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Components;
using MarquitoUtils.Web.React.Class.Components.Spinner;
using MarquitoUtils.Web.React.Class.Components.Toast;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Views
{
    /// <summary>
    /// Common class for views
    /// </summary>
    public abstract class WebView : WebClass
    {
        public static readonly string VIEW_NAME = "WebView";

        /// <summary>
        /// JS & css files, and js functions to implement
        /// </summary>
        public WebFileImport WebFileImport { get; private set; } 
            = new WebFileImport();
        /// <summary>
        /// The view title
        /// </summary>
        public string ViewTitle { get; set; } = "";
        /// <summary>
        /// Loading icon when we load something
        /// </summary>
        public EnumIcon LoadingIcon { get; set; } = EnumIcon.Loading9;
        /// <summary>
        /// The container id for the load
        /// </summary>
        public string ContainerIdForLoading { get; set; } = "";
        /// <summary>
        /// Do we load main web files ?
        /// </summary>
        protected bool LoadMainWebFileImport { get; set; } = false;
        /// <summary>
        /// A list of css variables to load on the view
        /// </summary>
        private Dictionary<string, string> CssVariables { get; } = new Dictionary<string, string>();
        /// <summary>
        /// Meta datas
        /// </summary>
        private Dictionary<string, string> MetaDatas { get; } = new Dictionary<string, string>();
        /// <summary>
        /// An object for display toast notifications
        /// </summary>
        public ToastManager ToastManager { get; set; }
        /// <summary>
        /// A waiting spinner
        /// </summary>
        public Spinner WaitingSpinner { get; set; }

        /// <summary>
        /// Common class for views
        /// </summary>
        /// <param name="webDataEngine">Web engine, contain data about session</param>
        protected WebView(WebDataEngine webDataEngine) : base(webDataEngine)
        {
            this.BeforeInit();
            if (this.LoadMainWebFileImport)
            {
                WebFileImport newWebFileImport = WebFileHelper.GetMainWebFileImport(this.LoadingIcon, this.ContainerIdForLoading);
                this.WebFileImport.Merge(newWebFileImport);
            }
            // Add js for this view (css is included inside js)
            this.WebFileImport.JavascriptModules
                .Add($"/dist{FileHelper.GetRelativePathOfClass(this.GetType(), ".Views.", 6)}", new List<string>());
            this.Init();
        }

        /// <summary>
        /// Common class for views
        /// </summary>
        /// <param name="webContext">Http context for build WebDataEngine</param>
        protected WebView(HttpContext webContext) : this(new WebDataEngine(webContext))
        {

        }

        /// <summary>
        /// This method is executed before constructor loading
        /// </summary>
        protected abstract void BeforeInit();

        /// <summary>
        /// This method is executed after constructor loading
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// This method can be called after initialisation of the view
        /// </summary>
        protected virtual void AfterInit()
        {

        }

        /// <summary>
        /// Get access url
        /// </summary>
        /// <returns>Access url</returns>
        public string GetUrlAccess()
        {
            StringBuilder sbUrl = new StringBuilder();

            sbUrl.Append(this.GetType().FullName)
                .Replace(this.GetType().Assembly.GetName().Name, "")
                .Remove(0, 1)
                .Replace('.', '/');

            return sbUrl.ToString();
        }

        /// <summary>
        /// Get a react component with enum field key
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <returns></returns>
        public HtmlString GetReactComponent(Enum fieldKey)
        {
            HtmlString componentReactJson = new HtmlString("");

            Component component = this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(field => Utils.IsNotNull(field.GetValue(this)))
                .Where(field => field.GetValue(this).GetType().IsSubclassOf(typeof(Component))
                || field.GetValue(this).GetType().IsEquivalentTo(typeof(Component)))
                .Where(field =>
                {
                    FieldName fieldName = (FieldName)field.GetCustomAttribute(typeof(FieldName));

                    return Utils.IsNotNull(fieldName)
                        && Utils.GetAsInteger(fieldKey).Equals(fieldName.FieldNameEnum);
                })
                .Select(field => (Component)field.GetValue(this))
                .FirstOrDefault();

            if (Utils.IsNotNull(component))
            {
                componentReactJson = component.GetAsReactJson();
            }

            return componentReactJson;
        }

        #region CSS

        /// <summary>
        /// Add a CSS variable
        /// </summary>
        /// <param name="cssVariableName">CSS variable name</param>
        /// <param name="cssVariableValue">CSS variable value</param>
        protected void AddCssVariable(string cssVariableName, string cssVariableValue)
        {
            this.CssVariables.Add(cssVariableName, cssVariableValue);
        }

        /// <summary>
        /// Get CSS variables inside style tag as HTML string
        /// </summary>
        /// <returns>CSS variables inside style tag</returns>
        public HtmlString GetCssVariables()
        {
            StringBuilder sbCss = new StringBuilder();

            sbCss.Append("<style type=\"text/css\">").Append("\n").Append(":root {").Append("\n")
                .Append(string.Join("\n", this.CssVariables
                .Select(metaData => this.GetCssVariable(metaData.Key, metaData.Value))))
                .Append("\n").Append("}").Append("\n").Append("</style>");

            return new HtmlString(sbCss.ToString());
        }

        /// <summary>
        /// Get CSS variable as string
        /// </summary>
        /// <param name="cssVariableName">CSS variable name</param>
        /// <param name="cssVariableValue">CSS variable value</param>
        /// <returns>CSS variable as string</returns>
        private string GetCssVariable(string cssVariableName, string cssVariableValue)
        {
            return $"\t--{cssVariableName}: {cssVariableValue};";
        }

        #endregion CSS

        #region MetaData

        /// <summary>
        /// Add meta data to your page
        /// </summary>
        /// <param name="metaName">Meta data name</param>
        /// <param name="metaContent">Meta data value</param>
        protected void AddMetaData(string metaName, string metaContent)
        {
            this.MetaDatas.Add(metaName, metaContent);
        }

        /// <summary>
        /// Add meta data to your page
        /// </summary>
        /// <param name="keyWords">A list of key words</param>
        protected void AddKeyWordsMetaData(List<string> keyWords)
        {
            this.MetaDatas.Add("keywords", string.Join(" ", keyWords));
        }

        /// <summary>
        /// Get meta data tag as HTML
        /// </summary>
        /// <returns>Meta data tag as HTML</returns>
        public HtmlString GetMetaDatas()
        {
            return new HtmlString(string.Join("\n", this.MetaDatas
                .Select(metaData => this.GetMetaData(metaData.Key, metaData.Value))));
        }

        /// <summary>
        /// Get meta data tag as string
        /// </summary>
        /// <param name="metaName">Meta data name</param>
        /// <param name="metaContent">Meta data value</param>
        /// <returns>Meta data tag as string</returns>
        private string GetMetaData(string metaName, string metaContent)
        {
            return $"<meta name=\"{metaName}\" content=\"{metaContent}\" />";
        }

        #endregion

        protected void InitToastManager()
        {
            this.ToastManager = new ToastManager("toastManager");
        }

        protected void InitWaitingSpinner()
        {
            this.WaitingSpinner = new Spinner("waitingSpinner", EnumIcon.Loading2);
        }
    }

    /// <summary>
    /// Field attribute
    /// </summary>
    public class FieldName : Attribute
    {
        public int FieldNameEnum { get; private set; }

        public FieldName(int fieldNameEnum)
        {
            this.FieldNameEnum = fieldNameEnum;
        }
    }
}
