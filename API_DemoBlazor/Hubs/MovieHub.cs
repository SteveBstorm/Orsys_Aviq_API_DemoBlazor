using Microsoft.AspNetCore.SignalR;

namespace API_DemoBlazor.Hubs
{
    public class MovieHub : Hub
    {
        public async Task NewMovie()
        {
            if(Clients != null)
                await Clients.All.SendAsync("NotifyNewMovie");
        }
    }
}
