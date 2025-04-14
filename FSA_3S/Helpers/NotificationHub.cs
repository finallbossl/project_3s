using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FSA_3S.Helpers
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("ReceiveNotification", message);
            }
            else
            {
                await Clients.All.SendAsync("ReceiveNotification", message);
            }
        }
    }
}