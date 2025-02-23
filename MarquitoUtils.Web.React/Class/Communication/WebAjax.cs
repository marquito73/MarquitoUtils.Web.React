using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.AspNetCore.Mvc;

namespace MarquitoUtils.Web.React.Class.Communication
{
    /// <summary>
    /// A web ajax, a class herited of this class can be called by an ajax request and return a string
    /// </summary>
    public abstract class WebAjax : WebClass
    {
        /// <summary>
        /// A web ajax, a class herited of this class can be called by an ajax request and return a string
        /// </summary>
        /// <param name="webDataEngine">A web tool for recover data stored inside session</param>
        public WebAjax(WebDataEngine webDataEngine) : base(webDataEngine)
        {
            
        }

        /// <summary>
        /// The exec method, in the inherited class, you can put some stuff inside
        /// </summary>
        /// <param name="action">The action you want to make</param>
        /// <param name="actionParameters">The ajax's parameters</param>
        /// <returns>Return the ajax result</returns>
        public abstract IActionResult Exec(string ajaxAction);
    }
}
