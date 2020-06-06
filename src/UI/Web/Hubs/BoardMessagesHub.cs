using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Web.Messages;

namespace Web.Hubs
{
    public class BoardMessagesHub : Hub
    {
        public async Task SendMessage(BoardMessage boardMessage)
        {
            await Clients.All.SendAsync("Send", boardMessage);
        }
    }
}
