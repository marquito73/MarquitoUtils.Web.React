using MarquitoUtils.Main.Class.Tools;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class WebFileImport
    {
        private readonly string RC = "\n";
        private readonly string TAB = "\n";
        public ISet<string> JavascriptScripts { get; set; } = new HashSet<string>();
        public IDictionary<string, List<string>> JavascriptModules { get; set; } = new Dictionary<string, List<string>>();
        public ISet<string> JsFunctions { get; set; } = new HashSet<string>();
        public ISet<string> Css { get; set; } = new HashSet<string>();

        public WebFileImport()
        {

        }

        public HtmlString GetJavascriptScripts()
        {
            StringBuilder sbJsImport = new StringBuilder();

            foreach (string importJs in this.JavascriptScripts)
            {
                sbJsImport.Append($"<script src=\"{importJs}.js\" defer></script>");
            }

            return new HtmlString(sbJsImport.ToString());
        }

        public HtmlString GetJavascriptModules()
        {
            StringBuilder sbJavascriptModules = new StringBuilder();

            foreach (KeyValuePair<string, List<string>> javascriptModule in this.JavascriptModules)
            {
                sbJavascriptModules.Append($"<script type=\"module\" src=\"{javascriptModule.Key}.js\" defer>").Append(this.RC);

                if (Utils.IsNotEmpty(javascriptModule.Value))
                {
                    javascriptModule.Value.ForEach(moduleExport =>
                    {
                        sbJavascriptModules.Append(this.TAB)
                            .Append($"import {{{moduleExport}}} from \"{javascriptModule.Key}.js\";").Append(this.RC);

                        sbJavascriptModules.Append(this.TAB)
                            .Append($"window.{moduleExport} = {moduleExport};").Append(this.RC);
                    });
                }

                sbJavascriptModules.Append("</script>").Append(this.RC);
            }

            return new HtmlString(sbJavascriptModules.ToString());
        }

        public HtmlString GetJavascriptFunctions()
        {
            StringBuilder sbJsFunctions = new StringBuilder();

            sbJsFunctions.Append("<script id='initFunctions'>").Append(this.RC)
                .Append("// Init functions").Append(this.RC)
                .Append("$( document ).ready(() => {").Append(this.RC);

            foreach (string jsFunctions in this.JsFunctions)
            {
                    sbJsFunctions.Append(jsFunctions).Append(this.RC);
            }
            sbJsFunctions.Append("})").Append(this.RC)
                .Append("</script>").Append(this.RC);

            return new HtmlString(sbJsFunctions.ToString());
        }

        public HtmlString GetCss()
        {
            StringBuilder sbCssImport = new StringBuilder();

            foreach (string importCss in this.Css)
            {
                sbCssImport.Append("<link rel=\"stylesheet\" href=\"")
                    .Append(importCss)
                    .Append(".css\" media=\"print\" onload=\"this.media='all'\">").Append(this.RC);
            }

            return new HtmlString(sbCssImport.ToString());
        }

        public void ClearAllLists()
        {
            this.JavascriptScripts.Clear();
            this.JavascriptModules.Clear();
            this.Css.Clear();
            this.JsFunctions.Clear();
        }

        public void Merge(WebFileImport otherWebFileImport)
        {
            otherWebFileImport.Css.ToList().ForEach(css => this.Css.Add(css));
            otherWebFileImport.JavascriptScripts.ToList().ForEach(js => this.JavascriptScripts.Add(js));
            otherWebFileImport.JavascriptModules.ToList().ForEach(this.JavascriptModules.Add);
            otherWebFileImport.JsFunctions.ToList().ForEach(js => this.JsFunctions.Add(js));
        }
    }
}
