using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DusColl
{
    public class SignalRMon : Hub
    {
        
        [HubMethodName("notifyChanges")]
        public static void NotifyChanges()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRMon>();
            context.Clients.All.displaynotifyChanges();
        }


    }
}