using Microsoft.AspNetCore.SignalR;
using Web_App.ModelDTOs;
using Web_App.Models;
using Web_App.Services.Interfaces;

namespace Web_App.Hubs
{
    public sealed class ChatHub: Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("StartMessage", $"{Context.ConnectionId} has joined");
            await base.OnConnectedAsync();
        }


        public async Task GroupMessage(CreateMessageDTO model)
        {
            var message = await _messageService.CreateMessage(model);

            if (message != null)
            {
                var result = new ShowMessageDTO
                {
                    Id = message.Id,
                    UserId = message.User.Id,
                    GroupId = message.Group.Id,
                    UserName = message.User.UserName ?? "",
                    MessageText = message.MessageText ?? "",
                };

                await Clients.Group(result.GroupId.ToString()).SendAsync("GroupMessage", result);
            }
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
