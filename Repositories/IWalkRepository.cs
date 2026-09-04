using NZworks.Models.Domain;

namespace NZworks.Repositories
{

    public interface IWalkRepository
    {
        Task<Walk> AddWalk(Walk walk);

        Task<Walk> GetWalkById(Guid id);

        Task<bool> Delete(Guid id);
    }

}
