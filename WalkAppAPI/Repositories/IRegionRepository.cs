using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllRegions();
    Task<Region?> GetRegionById(Guid id);
    Task<Region> CreateRegion(Region region);
    Task<Region?> UpdateRegion(Guid id, Region region);
    Task DeleteRegion(Guid id);
}