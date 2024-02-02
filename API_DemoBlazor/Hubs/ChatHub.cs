using API_DemoBlazor.Models;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace API_DemoBlazor.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) 
        {
            await Clients.All.SendAsync("NotifyNewMessage",message);
        }

        public async Task JoinGroup(string groupName)
        {   
            
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await SendToGroup(groupName, new Message
            {
                Author = "System",
                Content = $"L'utilisateur avec l'id : {Context.ConnectionId} vient d'arriver"
            });
        }

        public async Task SendToGroup(string groupName, Message message)
        {
            await Clients.Group(groupName).SendAsync("NotifyFrom"+groupName, message);
        }
    }
}
