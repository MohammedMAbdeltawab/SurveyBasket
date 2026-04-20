

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
    , IMapper mapper
    ) : ControllerBase
{


    [HttpGet("getAll")]
    public async Task<IActionResult> GetPolls(CancellationToken cancellationToken)
    {
        var polls = await pollService.GetAllAsync(cancellationToken);
        var response = polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(polls);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] int Id, CancellationToken cancellationToken)
    {
        Poll? poll = await pollService.GetAsync(Id,cancellationToken);

        var response = mapper.Map<PollResponse>(poll);
        return Ok(response);
    }
    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] PollRequest poll, CancellationToken cancellationToken)
    {

        var newPoll = await pollService.AddAsync(poll.Adapt<Poll>(), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] PollRequest poll, CancellationToken cancellationToken)
    {
        bool isUpdated = await pollService.UpdateAsync(Id, poll.Adapt<Poll>(), cancellationToken);
        if (!isUpdated)
        {
            return NotFound();
        }
        return NoContent();

    }
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
    {
        bool isDeleted = await pollService.DeleteAsync(Id, cancellationToken);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublish([FromRoute]int id, CancellationToken cancellationToken) { 
    bool toggledPublish=await pollService.TogglePublishStatusAsync(id, cancellationToken);
        if (!toggledPublish) {
            return NotFound();
        }
        return NoContent();
    }

}
