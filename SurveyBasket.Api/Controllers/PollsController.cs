

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
        return Ok(pollService.GetAll());
    }
    [HttpGet("{Id}")]
    public IActionResult Get(int Id)
    {
        Poll? poll = pollService.Get(Id);
        return (poll is null) ?
             NotFound("Poll Not Exist") : Ok(poll);
    }
    [HttpPost("")]
    public IActionResult Add(Poll poll) {
        Poll? newPoll = pollService.Add(poll);
        return CreatedAtAction(nameof(Get),new { id=newPoll.Id},newPoll);
    }
    [HttpPut("{Id}")]
    public IActionResult Update(int Id, Poll poll) {
        bool isUpdated = pollService.Update(Id, poll);
        if (!isUpdated) {
            return NotFound();
        }
        return NoContent();
    
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id) {
        bool isDeleted = pollService.Delete(Id);
        if (!isDeleted) {
            return NotFound();
        }
        return NoContent();
    }
}
