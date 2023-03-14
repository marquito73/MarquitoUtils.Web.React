﻿using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.General
{
    public class WebFileImport
    {
        private readonly string RC = "\n";
        private readonly string TAB = "\n";
        public List<string> ImportJs { get; set; } = new List<string>();
        public List<string> JsFunctions { get; set; } = new List<string>();
        public List<string> ImportCss { get; set; } = new List<string>();

        public WebFileImport()
        {

        }

        public HtmlString GetJavascriptImport()
        {
            StringBuilder sbJsImport = new StringBuilder();

            foreach (string importJs in this.ImportJs)
            {
                sbJsImport.Append("<script type=\"text/javascript\" src=\"")
                    .Append(importJs)
                    .Append("\" charset=\"utf-8\" defer/></script>").Append(this.RC);
            }

            return new HtmlString(sbJsImport.ToString());
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

        public HtmlString GetCssImport()
        {
            StringBuilder sbCssImport = new StringBuilder();

            foreach (string importCss in this.ImportCss)
            {
                /*sbCssImport.Append("<link rel=\"preload\" href=\"")
                    .Append(importCss)
                    .Append("\" as=\"style\">\n");*/
                sbCssImport.Append("<link rel=\"stylesheet\" href=\"")
                    .Append(importCss)
                    .Append("\" media=\"print\" onload=\"this.media='all'\">").Append(this.RC);
            }

            //< link rel = "preload" href = "/css/div.css" as= "style" >
            //< link rel = "stylesheet" href = "/css/div.css" media = "print" onload = "this.media='all'" >

            return new HtmlString(sbCssImport.ToString());
        }

        public void ClearAllLists()
        {
            this.ImportJs.Clear();
            this.ImportCss.Clear();
            this.JsFunctions.Clear();
        }

        public void Merge(WebFileImport otherWebFileImport)
        {
            this.ImportCss.AddRange(otherWebFileImport.ImportCss);
            this.ImportJs.AddRange(otherWebFileImport.ImportJs);
            this.JsFunctions.AddRange(otherWebFileImport.JsFunctions);
        }
    }
}
