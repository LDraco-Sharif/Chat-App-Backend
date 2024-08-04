using Microsoft.AspNetCore.SignalR;

namespace Web_App.Hubs
{
    public sealed class ChatHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("StartMessage", $"{Context.ConnectionId} has joined");
            await base.OnConnectedAsync();
        }


        public async Task GroupMessage(string user,int groupId, string message)
        {
            await Clients.Group(groupId.ToString()).SendAsync("GroupMessage", user, message);
        }

        public async Task AddToGroup(string userId, int groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
        }

        //public async Task GroupMessage(string user, int groupId, string message)
        //{
        //    await Clients.All.SendAsync("GroupMessage", user, message);
        //}
    }
}
