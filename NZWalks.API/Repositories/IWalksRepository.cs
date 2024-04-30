using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {

        Task<Walks> CreateAsync(Walks walk);

        Task<List<Walks>> GetAllAsync(string? filterOn = null , string? filterQuery = null);

        Task<Walks?> UpdateAsync(Guid id, Walks walk);

        Task<Walks?> GetByIdAsync(Guid id);

        Task<Walks?> DeleteAsync(Guid id);
    }
}
