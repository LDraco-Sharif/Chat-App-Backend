using Web_App.ModelDTOs;
using Web_App.Models;

namespace Web_App.Services.Interfaces;

public interface IMessageService
{
    Task<Message?> CreateMessage(CreateMessageDTO model);
    Task<List<ShowMessageDTO>?> GetMessages(string userId, int groupId, int? startingId = null, int quantity = 10);
}
