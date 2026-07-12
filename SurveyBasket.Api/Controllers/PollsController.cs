using SurveyBasket.Api.Contracts.Requests;
using SurveyBasket.Api.Mapping;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("GetAll")]
    public IActionResult GetAll() {
        return Ok(_pollService.GetAll().MapToResponse());
    }
    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id) {
        var poll = _pollService.Get(id);
        if (poll is null) {
            return NotFound();
        }
        return Ok(poll.MapToResponse());
    }
    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request) { 
    var newPoll= _pollService.Add(request.MapToPoll());
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll); 
    }
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequest request) {
        var isUpdated = _pollService.Update(id, request.MapToPoll());
            return isUpdated ? NoContent() : NotFound();  // Return 204 No Content if updated, 404 Not Found if not found   
    }


    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id) {
        var isDeleted = _pollService.Delete(id);
        return isDeleted ? NoContent() : NotFound();
    }
}
