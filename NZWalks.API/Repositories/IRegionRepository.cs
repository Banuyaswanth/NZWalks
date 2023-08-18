using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> CreateRegionAsync(Region newRegionDetails);
        Task<Region?> GetRegionByCodeAsync(string code);
        Task<List<Region>> GetAllRegionsAsync();
    }
}
