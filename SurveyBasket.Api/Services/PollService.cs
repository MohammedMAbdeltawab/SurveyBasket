namespace SurveyBasket.Api.Services;

public class PollService(ApplicationDbContext context) : IPollService
{

    public  async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken) =>await context.polls.AsNoTracking().ToListAsync();
    public async Task<Poll?>GetAsync(int Id, CancellationToken cancellationToken) => await context.polls.SingleOrDefaultAsync(p => p.Id == Id);

    public async Task<Poll?> AddAsync  (Poll poll, CancellationToken cancellationToken) {
        await context.polls.AddAsync(poll);
        await context.SaveChangesAsync(cancellationToken);
        return poll;
    }
    public async Task<bool>  UpdateAsync(int Id, Poll poll, CancellationToken cancellationToken) {


        Poll? currentPoll =await GetAsync(Id, cancellationToken);
        if (currentPoll is null) { return false; }
        currentPoll.Title = poll.Title;
        currentPoll.Summary = poll.Summary;
        currentPoll.StartsAt = poll.StartsAt;
        currentPoll.EndsAt = poll.EndsAt;
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken)
    {
        Poll? poll = await GetAsync(Id, cancellationToken);
        if (poll is null) { return false; }
        context.polls.Remove(poll);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken) { 
    Poll? currentPoll = await GetAsync(id, cancellationToken);  
            currentPoll.isPublished=!currentPoll.isPublished;
        await context.SaveChangesAsync(cancellationToken);  
        return true;
    
    }

}
