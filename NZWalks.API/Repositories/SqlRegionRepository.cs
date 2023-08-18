using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SqlRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateRegionAsync(Region newRegionDetails)
        {
            await dbContext.Regions.AddAsync(newRegionDetails);
            await dbContext.SaveChangesAsync();
            return newRegionDetails;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByCodeAsync(string code)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
