using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MarquitoUtils.Web.React.Class.NotifyHub
{
    /// <summary>
    /// Proxy for access to the signalR notify hub
    /// </summary>
    public sealed class NotifyHubProxy
    {
        /// <summary>
        /// The notify hub, for send and get data from client
        /// </summary>
        protected IHubContext<NotifyHub> NotifyHub { get; set; }

        /// <summary>
        /// Proxy for access to the signalR notify hub
        /// </summary>
        public NotifyHubProxy(IHubContext<NotifyHub> notifyHub)
        {
            this.NotifyHub = notifyHub;
        }

        /// <summary>
        /// Send data to all connected clients
        /// </summary>
        /// <typeparam name="IData">Data type</typeparam>
        /// <param name="methodName">Method name to call</param>
        /// <param name="data">Values to pass to clients</param>
        public void SendDataToAllClients<IData>(string methodName, Dictionary<string, IData> data) 
            where IData : class
        {
            this.NotifyHub.Clients.All.SendAsync(methodName, data);
        }

        /// <summary>
        /// Send data to a connected client
        /// </summary>
        /// <typeparam name="IData">Data type</typeparam>
        /// <param name="methodName">Method name to call</param>
        /// <param name="data">Values to pass to clients</param>
        /// <param name="connectionID">Connection ID</param>
        public void SendDataToClient<IData>(string methodName, Dictionary<string, IData> data, string connectionID) 
            where IData : class
        {
            this.NotifyHub.Clients.Client(connectionID).SendAsync(methodName, data);
        }

        /// <summary>
        /// Send data to a connected user
        /// </summary>
        /// <typeparam name="IData">Data type</typeparam>
        /// <param name="methodName">Method name to call</param>
        /// <param name="data">Values to pass to clients</param>
        /// <param name="userID">User ID</param>
        public void SendDataToUser<IData>(string methodName, Dictionary<string, IData> data, string userID)
            where IData : class
        {
            this.NotifyHub.Clients.User(userID).SendAsync(methodName, data);
        }

        /// <summary>
        /// Send data to a group
        /// </summary>
        /// <typeparam name="IData">Data type</typeparam>
        /// <param name="methodName">Method name to call</param>
        /// <param name="data">Values to pass to clients</param>
        /// <param name="groupName">Group name</param>
        public void SendDataToClientGroup<IData>(string methodName, Dictionary<string, IData> data, string groupName) 
            where IData : class
        {
            this.NotifyHub.Clients.Group(groupName).SendAsync(methodName, data);
        }
    }
}
