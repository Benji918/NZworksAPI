using Microsoft.EntityFrameworkCore;
using NZworks.Data;
using NZworks.Models.Domain;
using NZworks.Models.DTO;

namespace NZworks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzWalksDBContext _dbcontext;

        public SQLRegionRepository(NzWalksDBContext dBcontext) => _dbcontext = dBcontext;

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbcontext.Regions.AddAsync(region);
            await _dbcontext.SaveChangesAsync();
            return region;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingregion = await _dbcontext.Regions.FindAsync(id);

            if (existingregion is null)
            {
                return false;
            }


            _dbcontext.Regions.Remove(existingregion);
            await _dbcontext.SaveChangesAsync();

            return true;

        }


        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await _dbcontext.Regions.FindAsync(id);

            if (existingregion == null)
            {
                return null;
            }

            // Update the region properties
            existingregion.Name = region.Name;
            existingregion.Code = region.Code;
            existingregion.RegionImageUrl = region.RegionImageUrl;

            await _dbcontext.SaveChangesAsync();

            return existingregion;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbcontext.Regions.ToListAsync();
        }

        public async Task<Region> GetById(Guid id)
        {
            return await _dbcontext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }


    }
}
