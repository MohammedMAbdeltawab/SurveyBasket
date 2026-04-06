namespace SurveyBasket.Api.Services;

public class PollService : IPollService
{
    public static List<Poll> _polls = [
        new Poll(){ Id=1,Title="poll 1",Description="Description for poll 1"},
        new Poll(){ Id=2,Title="poll 2",Description="Description for poll 2"},
        new Poll(){ Id=3,Title="poll 3",Description="Description for poll 3"},
        new Poll(){ Id=4,Title="poll 4",Description="Description for poll 4"},
        ];
    public IEnumerable<Poll> GetAll() => _polls.ToList();
    public Poll? Get(int Id) => _polls.SingleOrDefault(p => p.Id == Id);

    public Poll? Add(Poll poll)
    {
        poll.Id = _polls.Count + 1;
        _polls.Add(poll);
        return poll;
    }
    public bool Update(int Id, Poll poll) {
        Poll currentPoll = Get(Id);
        if (currentPoll is null) { return false; }
        currentPoll.Title = poll.Title;
        currentPoll.Description = poll.Description;
        return true;
    }
    public bool Delete(int Id)
    {
        Poll poll = Get(Id);
        if (poll is null) { return false; }
        _polls.Remove(poll);
        return true;
    }
}
