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

        public async Task<List<Walk>> GetAllWalks()
        {
            return await _dbcontext.Walks
                         .Include(d => d.Difficulty)
                         .Include(r => r.Region)
                         .ToListAsync();
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
