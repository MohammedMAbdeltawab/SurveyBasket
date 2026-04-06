

using SurveyBasket.Api.Contracts.Polls.Requests;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(
    [FromKeyedServices("poll")] IPollService pollService
    ) : ControllerBase
{


    [HttpGet("getAll")]
    public IActionResult GetPolls()
    {
        var polls = pollService.GetAll();
        return Ok(polls.MapToResponse());
    }
    [HttpGet("{Id}")]
    public IActionResult Get([FromRoute] int Id)
    {
        Poll? poll = pollService.Get(Id);
        return (poll is null) ?
             NotFound("Poll Not Exist") : Ok(poll.MapToResponse());
    }
    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest poll) {
        var newPoll = pollService.Add(poll.MapToPoll());
        return CreatedAtAction(nameof(Get),new { id=newPoll.Id},newPoll);
    }
    [HttpPut("{Id}")]
    public IActionResult Update([FromRoute] int Id, [FromBody] CreatePollRequest poll) {
        bool isUpdated = pollService.Update(Id, poll.MapToPoll());
        if (!isUpdated) {
            return NotFound();
        }
        return NoContent();
    
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete([FromRoute]int Id) {
        bool isDeleted = pollService.Delete(Id);
        if (!isDeleted) {
            return NotFound();
        }
        return NoContent();
    }
}
