using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarquitoUtils.Web.React.Class.NotifyHub
{
    /// <summary>
    /// Notify hub, with SignalR
    /// </summary>
    public sealed class NotifyHub : Hub
    {
        /*public void newMessage(string userName, string message)
        {

        }*/

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}