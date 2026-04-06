namespace SurveyBasket.Api.Interfaces;

public interface IPollService
{
    public IEnumerable<Poll> GetAll();
    public Poll? Get(int Id);
    public Poll? Add(Poll poll);
    public bool Update(int Id,Poll poll);
    public bool Delete(int ID);
}
