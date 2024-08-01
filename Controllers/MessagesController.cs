using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_App.Data;
using Web_App.ModelDTOs;
using Web_App.Models;
using Web_App.Services.Interfaces;

namespace Web_App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }


    // GET: api/Messages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessages(string userId, int groupId, int? startingId = null, int quantity = 10)
    {
        var result = await _messageService.GetMessages(userId, groupId, startingId, quantity);

        if(result == null)
        {
            return BadRequest("Issue fetching Group Messages");
        }
        else
        {
            return Ok(result); 
        }
    }

    // GET: api/Messages/5
  //  [HttpGet("{id}")]
    //public async Task<ActionResult<Message>> GetMessage(int id)
    //{
    //    var message = await _context.Messages.FindAsync(id);

    //    if (message == null)
    //    {
    //        return NotFound();
    //    }

    //    return message;
    //}

    // PUT: api/Messages/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutMessage(int id, Message message)
    //{
    //    if (id != message.Id)
    //    {
    //        return BadRequest();
    //    }

    //   // _context.Entry(message).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!MessageExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    // POST: api/Messages
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostMessage([FromBody] CreateMessageDTO model)
    {
        var message = await _messageService.CreateMessage(model);
        if(message != null)
        {
            var result = new ShowMessageDTO
            {
                Id = message.Id,
                UserId = message.User.Id,
                GroupId = message.Group.Id,
                UserName = message.User.UserName ?? "",
                MessageText = message.MessageText ?? "",
            };

            return Ok(result);
        }
        else
        {
            return BadRequest("Issue Creating Message");
        }
    }

    //// DELETE: api/Messages/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteMessage(int id)
    //{
    //    var message = await _context.Messages.FindAsync(id);
    //    if (message == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Messages.Remove(message);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool MessageExists(int id)
    //{
    //    return _context.Messages.Any(e => e.Id == id);
    //}
}
