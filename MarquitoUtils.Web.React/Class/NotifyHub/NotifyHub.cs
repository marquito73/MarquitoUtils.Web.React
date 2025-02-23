using Microsoft.AspNetCore.SignalR;

namespace MarquitoUtils.Web.React.Class.NotifyHub
{
    /// <summary>
    /// Notify hub, with SignalR
    /// </summary>
    public abstract class NotifyHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            this.OnConnected(this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            this.OnDisconnected(this.Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Event handled when a user is connected
        /// </summary>
        /// <param name="userConnectionID">The user connection ID</param>
        protected abstract void OnConnected(string userConnectionID);

        /// <summary>
        /// Event handled when a user is disconnected
        /// </summary>
        /// <param name="userConnectionID">The user connection ID</param>
        protected abstract void OnDisconnected(string userConnectionID);

        /// <summary>
        /// Send data to a group
        /// </summary>
        /// <typeparam name="T">The type of the data to send</typeparam>
        /// <param name="groupName">The group name</param>
        /// <param name="methodName">The method name</param>
        /// <param name="data">The data</param>
        protected void SendDataToClientGroup<T>(string groupName, string methodName, T data)
        {
            this.Clients.Group(groupName).SendAsync(methodName, data);
        }
    }
}