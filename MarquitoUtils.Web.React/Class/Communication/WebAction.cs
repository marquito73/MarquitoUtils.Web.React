using MarquitoUtils.Web.React.Class.Enums.Action;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Mvc;

namespace MarquitoUtils.Web.React.Class.Communication
{
    /// <summary>
    /// A web action, a class herited of this class can be called by an action POST request and return a string
    /// </summary>
    public abstract class WebAction : WebClass
    {
        /// <summary>
        /// A web action, a class herited of this class can be called when call view or post data
        /// </summary>
        /// <param name="webDataEngine">A web tool for recover data stored inside session</param>
        public WebAction(WebDataEngine webDataEngine) : base(webDataEngine)
        {

        }

        /// <summary>
        /// The exec method, in the inherited class, you can put some stuff inside
        /// </summary>
        /// <param name="action">The action you want to make</param>
        /// <returns></returns>
        public abstract IActionResult Exec(EnumAction action);
    }
}
