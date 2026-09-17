using NZworks.Models.Domain;

namespace NZworks.Repositories
{

    public interface IWalkRepository
    {
        Task<Walk> AddWalk(Walk walk);

        Task<Walk> GetWalkById(Guid id);

        Task<bool> Delete(Guid id);

        Task<List<Walk>> GetAllWalks(string? name, Guid? regionId,
                                        Guid? difficultyId, bool? ascending
                                        );

        Task<bool> RegionExists(Guid id);

        Task<bool> DifficultyExists(Guid id);
    }

}
