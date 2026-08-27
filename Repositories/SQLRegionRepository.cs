using Microsoft.EntityFrameworkCore;
using NZworks.Data;
using NZworks.Models.Domain;

namespace NZworks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzWalksDBContext _dbcontext;

        public SQLRegionRepository(NzWalksDBContext dBcontext)
        {
            _dbcontext = dBcontext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbcontext.Regions.ToListAsync();
        }
    }
}
