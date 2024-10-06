using Microsoft.AspNetCore.SignalR;

namespace BookStore.Hubs
{
    public class SalesHub : Hub
    {
        public async Task SalesNotification(String sales)
        {
            await Clients.All.SendAsync("ReceiveSalesNotification", sales);
        }
    }
}
