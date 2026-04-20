namespace SurveyBasket.Api.Interfaces;

public interface IPollService
{
    public Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Poll?> GetAsync(int Id, CancellationToken cancellationToken);
    public Task<Poll?> AddAsync(Poll poll, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(int Id, Poll poll, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken);
    public Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken);
}
