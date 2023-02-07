using MarquitoUtils.Web.Class.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Controllers
{
    public abstract class DefaultReactController : DefaultController
    {
        protected DefaultReactController(ILogger<DefaultController> logger) : base(logger)
        {
            this.MainAjaxLocationPath = "MarquitoUtils.Web.React.Class.Ajax";
            this.MainAssembly = Assembly.GetExecutingAssembly();
        }
    }
}
