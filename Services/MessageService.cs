using Microsoft.EntityFrameworkCore;
using Web_App.Data;
using Web_App.ModelDTOs;
using Web_App.Models;
using Web_App.Services.Interfaces;

namespace Web_App.Services;

public class MessageService : IMessageService
{
    private readonly WebAppContext _context;
    public MessageService(WebAppContext context)
    {
        _context = context;
    }
    public async Task<Message?> CreateMessage(CreateMessageDTO model)
    {
        var user = await _context.Users.FindAsync(model.UserId);
        if(user == null)
        {
            return null;
        }

        var group = await _context.Groups.FindAsync(model.GroupId);

        if(group == null)
        {
            return null;
        }

        Message message = new Message 
        {
            MessageText = model.MessageText,
            User = user,
            Group = group
        };

        await _context.Messages.AddAsync(message);
        _context.SaveChanges();

        return message;
    }

    public async Task<List<ShowMessageDTO>?> GetMessages(string userId, int groupId, int? startingId = null, int quantity = 10)
    {
        var user = await _context.Users.FindAsync(userId);
        if(user == null)
        {
            return null;
        }

        var group = await _context.Groups.Include(g => g.Users)
                                            .FirstOrDefaultAsync(g => g.Id == groupId);

        if (group == null || !group.Users.Contains(user))
        {
            return null;
        }

        var messageQuery = _context.Messages.Include(m => m.User)
                                            .Include(m => m.Group)
                                            .AsQueryable();

        if(startingId != null)
        {
            messageQuery = messageQuery.Where(m => m.Id < startingId);
        }

        var messages = await messageQuery.Where(m => m.Group.Id == groupId)
                                        .Take(quantity)
                                        .OrderByDescending(m => m.Id)
                                        .ToListAsync();

        List<ShowMessageDTO> resultMessages = new List<ShowMessageDTO>();
        foreach (var message in messages)
        {
            var resultMessage = new ShowMessageDTO()
            {
                Id = message.Id,
                UserId = message.User.Id ?? "",
                GroupId = groupId,
                UserName = message.User.UserName ?? "",
                MessageText = message.MessageText,
            };
            resultMessages.Add(resultMessage);
        }

        return resultMessages;
    }

}
