using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Attributes
{
    public class BlockBotsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString().ToLower();

            // If the "user" is a bot, an indexing bot, or a search engine, we send an HTTP 403
            if (userAgent.Contains("bot") || userAgent.Contains("crawl") || userAgent.Contains("spider") || userAgent == ""
                || !context.HttpContext.Request.Headers.ContainsKey("Cookie"))
            {
                context.Result = new StatusCodeResult(403);
            }

            base.OnActionExecuting(context);
        }
    }
}
