using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Hubs
{
    public class LocalisationHub : Hub
    {
        public async Task SendLocation(double latitude, double longitude)
        {
            await Clients.All.SendAsync("ReceiveLocation", latitude, longitude);
        }
    }
}