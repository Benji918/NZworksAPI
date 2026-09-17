using Microsoft.EntityFrameworkCore;
using NZworks.Data;
using NZworks.Models.Domain;

namespace NZworks.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NzWalksDBContext _dbcontext;
        public SQLWalkRepository(NzWalksDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public async Task<bool> RegionExists(Guid id)
        {
            return await _dbcontext.Regions.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> DifficultyExists(Guid id)
        {
            return await _dbcontext.Difficulties.AnyAsync(d => d.Id == id);
        }

        public async Task<List<Walk>> GetAllWalks(string? name, Guid? regionId,
            Guid? difficultyId, bool? ascending)
        {
            var query = _dbcontext.Walks
                         .Include(d => d.Difficulty)
                         .Include(r => r.Region)
                         .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(w => w.Name.Contains(name));
            }

            if (regionId.HasValue)
            {
                query = query.Where(w => w.RegionId == regionId.Value);
            }

            if (difficultyId.HasValue)
            {
                query = query.Where(w => w.DifficultyId == difficultyId.Value);
            }

            if (ascending.HasValue)
            {
                query = ascending.Value ? query.OrderBy(w => w.Name) : query.OrderByDescending(w => w.Name);
            }

            return await query.ToListAsync();
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            await _dbcontext.Walks.AddAsync(walk);
            await _dbcontext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            var walk = await _dbcontext.Walks.FindAsync(id);


            if (walk == null)
            {
                return null;
            }

            return walk;
        }

        public async Task<bool> Delete(Guid id)
        {
            var walk = await _dbcontext.Walks.FindAsync(id);
            if (walk == null)
            {
                return false;
            }
            _dbcontext.Walks.Remove(walk);

            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
