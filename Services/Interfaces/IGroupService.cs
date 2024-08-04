using Web_App.Models;

namespace Web_App.Services.Interfaces;

public interface IGroupService
{
    Task<Group?> CreateGroup(User user, string groupName);
    Task<Group?> EnterGroup(User user, string groupName);
}
