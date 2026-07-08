namespace SurveyBasket.Api.Services;

public class PollService : IPollService
{
    private readonly static List<Poll> _polls = [
        new Poll{Id=1, Title="Poll 1", Description="Description 1"},
        new Poll{Id=2, Title="Poll 2", Description="Description 2"},     
        new Poll{Id=3, Title="Poll 3", Description="Description 3"}     
        ];

    public Poll Add(Poll newPoll)
    {
        newPoll.Id = _polls.Max(p => p.Id) + 1;
        _polls.Add(newPoll);
        return newPoll;
    }

    public bool Delete(int id)
    {
        var poll=Get(id); ;
        if (poll is null) { return false; }
        return _polls.Remove(poll);
    }

    public Poll? Get(int id)=>_polls.SingleOrDefault(p => p.Id == id);

    public IEnumerable<Poll> GetAll() => _polls;

    public bool Update(int id, Poll poll)
    {
       var currentPoll = Get(id);
        if (currentPoll is null) { return false; }
        currentPoll.Title = poll.Title;
        currentPoll.Description = poll.Description;
                return true;
    }
}
