using MarquitoUtils.Web.React.Class.NotifyHub;
using MarquitoUtils.Web.React.Class.Tools;

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
        public WebAction(WebDataEngine webDataEngine, NotifyHubProxy notifyHubProxy) : base(webDataEngine, notifyHubProxy)
        {

        }

        /// <summary>
        /// The exec method, in the inherited class, you can put some stuff inside
        /// </summary>
        /// <param name="action">The action you want to make</param>
        /// <returns></returns>
        public abstract string Exec(string action);
    }
}
