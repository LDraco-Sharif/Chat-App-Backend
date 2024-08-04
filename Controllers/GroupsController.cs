using Microsoft.AspNetCore.Mvc;
using Web_App.ModelDTOs;
using Web_App.Services.Interfaces;

namespace Web_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public GroupsController(IUserService userServices, IGroupService groupService)
        {
            _userService = userServices;
            _groupService = groupService;
        }

        [HttpPost("create-group")]
        public async Task<IActionResult> CreateGroup([FromBody] SignUpDTO model)
        {
            var user = await _userService.RegisterLogin(model);

            if (user == null)
            {
                return BadRequest(new { Message = "Problem with User Credentials" });
            }
            else
            {
                var groupResult = await _groupService.CreateGroup(user, model.GroupName);
                
                if (groupResult == null)
                {
                    return BadRequest(new { Message = "Group already exists" });
                }
                else
                {
                    var result = new
                    {
                        UserId = user.Id,
                        UserName = user.UserName ?? "",
                        GroupId = groupResult.Id,
                        GroupName = groupResult.Name
                    };
                    return Ok(result);
                }
            }
        }

        [HttpPost("enter-group")]
        public async Task<IActionResult> EnterGroup([FromBody] SignUpDTO model)
        {
            var user = await _userService.RegisterLogin(model);

            if (user == null)
            {
                return BadRequest(new { Message = "Problem with User Credentials" });
            }
            else
            {
                var groupResult = await _groupService.EnterGroup(user, model.GroupName);

                if (groupResult == null)
                {
                    return BadRequest(new { Message = "Group does not exist." });
                }
                else
                {
                    var result = new
                    {
                        UserId = user.Id,
                        UserName = user.UserName ?? "",
                        GroupId = groupResult.Id,
                        GroupName = groupResult.Name
                    };
                    return Ok(result);
                }
            }
        }
    }
}
