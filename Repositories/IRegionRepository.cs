using NZworks.Models.Domain;

namespace NZworks.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region> GetById(Guid id);

    }
}
