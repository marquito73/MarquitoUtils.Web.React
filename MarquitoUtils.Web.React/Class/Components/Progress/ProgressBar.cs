﻿using MarquitoUtils.Web.React.Class.Entities;
using MarquitoUtils.Web.React.Class.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Progress
{
    public class ProgressBar : Component
    {
        public float Percent { get; set; } = 0;
        public string ProgressColor { get; set; } = "";
        public EnumWebEvent ChangeElementEvent { get; set; } = EnumWebEvent.Click;
        public string ChangeElementId { get; set; } = "";
        public WebFunction ChangeValueFunction { get; set; } = new WebFunction();
        public bool HideValue { get; set; } = false;
        public ProgressBar(string id, float percent) : base(id)
        {
            this.Percent = percent;
        }

        public override HtmlString GetAsReactJson()
        {
            return new HtmlString(this.GetInitReactComponent());
        }
    }
}
