using MarquitoUtils.Main.Class.Entities.Param;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Components.General;
using MarquitoUtils.Web.React.Class.Components.Grid;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Views
{
    public abstract class WebView : WebClass
    {
        public static string VIEW_NAME = "WebView";

        public WebFileImport WebFileImport { get; private set; } 
            = new WebFileImport();
        public string ViewTitle { get; set; } = "";
        public EnumIcon LoadingIcon { get; set; } = EnumIcon.Loading9;
        public string ContainerIdForLoading { get; set; } = "";
        private List<string> ComponentAlreadyDeclared { get; set; } = new List<string>();
        protected bool LoadMainWebFileImport { get; set; } = false;
        protected WebView(WebDataEngine webDataEngine) : base(webDataEngine)
        {
            this.Init();
            if (this.LoadMainWebFileImport) 
            {
                WebFileImport newWebFileImport = WebFileHelper.GetMainWebFileImport(this.LoadingIcon, 
                    this.ContainerIdForLoading);
                newWebFileImport.Merge(this.WebFileImport);
                this.WebFileImport = newWebFileImport;
            }
        }
        protected WebView(HttpContext webContext) : this(new WebDataEngine(webContext))
        {

        }
        public abstract void Init();

        public string GetUrlAccess()
        {
            StringBuilder sbUrl = new StringBuilder();

            sbUrl.Append(this.GetType().FullName)
                .Replace(this.GetType().Assembly.GetName().Name, "")
                .Remove(0, 1)
                .Replace('.', '/');

            return sbUrl.ToString();
        }
    }
}
