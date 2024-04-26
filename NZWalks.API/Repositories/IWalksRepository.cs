using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {

        Task<Walks> CreateAsync(Walks walk);

        Task<List<Walks>> GetAllAsync();

        Task<Walks?> UpdateAsync(Guid id, Walks walk);

        Task<Walks?> GetById(Guid id);

        Task<Walks?> DeleteAsync(Guid id);
    }
}
