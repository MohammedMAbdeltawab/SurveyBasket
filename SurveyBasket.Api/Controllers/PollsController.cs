namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("GetAll")]
    public IActionResult GetAll() {
        return Ok(_pollService.GetAll());
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id) {
        var poll = _pollService.Get(id);
        if (poll is null) {
            return NotFound();
        }
        return Ok(poll);
    }
    [HttpPost("")]
    public IActionResult Add(Poll request) { 
    var newPoll= _pollService.Add(request);
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);  // Return 201 Created with the location of the new resource
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Poll request) {
        var isUpdated = _pollService.Update(id, request);
            return isUpdated ? NoContent() : NotFound();  // Return 204 No Content if updated, 404 Not Found if not found   
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var isDeleted = _pollService.Delete(id);
        return isDeleted ? NoContent() : NotFound();
    }
}
