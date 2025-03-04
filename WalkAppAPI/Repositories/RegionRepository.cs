using Microsoft.EntityFrameworkCore;
using WalkAppAPI.Data;
using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Repositories;

public class RegionRepository: IRegionRepository
{
    private readonly WalksDbContext _context;
    
    public RegionRepository(WalksDbContext dbContext)
    {
        _context = dbContext;    
    }

    public async Task<List<Region>> GetAllRegions()
    {
        return await _context.Regions.ToListAsync();
    }

    public async Task<Region?> GetRegionById(Guid id)
    {
        return await _context.Regions.FindAsync(id);
    }

    public async Task<Region> CreateRegion(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateRegion(Guid id, Region region)
    {
        var regionToUpdate = await _context.Regions.FindAsync(id);
        if (regionToUpdate == null) return null;
        
        regionToUpdate.Name = region.Name;
        regionToUpdate.Code = region.Code;
        regionToUpdate.RegionImageUrl = region.RegionImageUrl;
        await _context.SaveChangesAsync();
        return regionToUpdate;
    }

    public async Task<bool> DeleteRegion(Guid id)
    {
        var regionToDelete = await _context.Regions.FindAsync(id);
        if (regionToDelete == null) return false;
        _context.Regions.Remove(regionToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}