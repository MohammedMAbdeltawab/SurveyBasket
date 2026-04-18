

using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.Api.Contracts.Polls.Requests;
using SurveyBasket.Api.Contracts.Polls.Responses;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(
    [FromKeyedServices("poll")] IPollService pollService
    ,IMapper mapper
    ) : ControllerBase
{


    [HttpGet("getAll")]
    public IActionResult GetPolls() 
    {
        var polls = pollService.GetAll();
        var response = polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(polls);
    }
    [HttpGet("{Id}")]
    public IActionResult Get([FromRoute] int Id)
    {
        Poll? poll = pollService.Get(Id);
       
        var response=mapper.Map<PollResponse>(poll);
        return Ok(response);
    }
    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest poll) {
     
        var newPoll = pollService.Add(poll.Adapt<Poll>());
        return CreatedAtAction(nameof(Get),new { id=newPoll.Id},newPoll);
    }
    [HttpPut("{Id}")]
    public IActionResult Update([FromRoute] int Id, [FromBody] CreatePollRequest poll) {
        bool isUpdated = pollService.Update(Id, poll.Adapt<Poll>());
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
