using Microsoft.EntityFrameworkCore;
using Web_App.Data;
using Web_App.Models;
using Web_App.Services.Interfaces;

namespace Web_App.Services;

public class GroupService: IGroupService
{
    private readonly WebAppContext _context;
    public GroupService(WebAppContext context)
    {
        _context = context;
    }
    public async Task<Group?> CreateGroup(string groupName)
    {
        var group = await _context.Groups.Where(g =>  g.Name == groupName).FirstOrDefaultAsync();

        if(group == null)
        {
            Group newGroup = new()
            {
                Name = groupName
            };
            await _context.Groups.AddAsync(newGroup);
            _context.SaveChanges();
            return newGroup;
        }
        else
        {
            return null;
        }
    }
    public async Task<Group?> EnterGroup(User user, string groupName)
    {
        var group = await _context.Groups.Include(g => g.Users).Where(g => g.Name == groupName).FirstOrDefaultAsync();
        if(group == null)
        {
            return null;
        }
        else
        {
            if(!group.Users.Any(u => u.Id == user.Id)) 
            {
                group.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            
            return group;
        }

    }
}
