using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NeoSistem.Trinnk.Management.Hubs
{
    public static class UserList
    {
        static private Dictionary<string, string> _userlist = new Dictionary<string, string>();

        public static Dictionary<string, string> _UserList
        {
            get
            {
                return _userlist;
            }
        }
        public static void Add(string Nick, string ConnectionID)
        {
            _UserList[Nick] = ConnectionID;
        }

        public static void Remove(string ConnectionID)
        {
            string key = GetNickByConnectionID(ConnectionID);
            if (key != null)
            {
                _UserList.Remove(key);
            }
        }
        public static bool HasNick(string Nick)
        {
            return _UserList.Any(cl => cl.Key.ToUpper() == Nick.ToUpper());
        }
        public static string GetNickByConnectionID(string ConnectionID)
        {
            return _UserList.FirstOrDefault(cl => cl.Value == ConnectionID).Key;
        }
    }

    [HubName("systemHub")]
    public class System: Microsoft.AspNet.SignalR.Hub
    {
        public override Task OnConnected()
        {
            return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionID = Context.ConnectionId;
            UserList.Remove(connectionID);
            return Clients.All.SendAsync("SetListDisconnect", UserList._UserList);
            return base.OnDisconnected(stopCalled);
        }

        public Task AddUserList(string userName, string connectionID)
        {
            UserList.Add(userName, connectionID);
            return Clients.All.SendAsync("SetList", UserList._UserList);
        }

        public Task SendMessage(string message, int userId=0)
        {
            var connectionID = NeoSistem.Trinnk.Management.Hubs.UserList._UserList.FirstOrDefault(x => x.Key == userId.ToString()).Value;
            if (connectionID != null)
            {
                return Clients.Client(connectionID).SendAsync("receiveMessage",message);
            }
            else
            {
                return Clients.All.SendAsync("receiveMessage", message);
            }
        }
    }
}