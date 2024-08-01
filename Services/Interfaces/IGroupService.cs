using Web_App.Models;

namespace Web_App.Services.Interfaces;

public interface IGroupService
{
    Task<Group?> CreateGroup(string groupName);
    Task<Group?> EnterGroup(User user, string groupName);
}
