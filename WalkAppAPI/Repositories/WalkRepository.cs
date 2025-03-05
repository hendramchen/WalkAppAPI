using Microsoft.EntityFrameworkCore;
using WalkAppAPI.Data;
using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Repositories;

public class WalkRepository: IWalkRepository
{
    private readonly WalksDbContext _context;

    public WalkRepository(WalksDbContext context)
    {
        _context = context;
    }
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _context.Walks.AddAsync(walk);
        await _context.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await _context.Walks
            .Include("Difficulty")
            .Include("Region")
            .FirstOrDefaultAsync(field => field.Id == id);
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var walkToUpdate = await _context.Walks.FindAsync(id);
        if (walkToUpdate == null) return null;
        
        walkToUpdate.Name = walk.Name;
        walkToUpdate.Description = walk.Description;
        walkToUpdate.LengthInKm = walk.LengthInKm;
        walkToUpdate.WalkImageUrl = walk.WalkImageUrl;
        walkToUpdate.DifficultyId = walk.DifficultyId;
        walkToUpdate.RegionId = walk.RegionId;
        
        await _context.SaveChangesAsync();
        return walkToUpdate;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var walkToDelete = await _context.Walks.FindAsync(id);
        if (walkToDelete == null) return false;
        _context.Walks.Remove(walkToDelete);
        await _context.SaveChangesAsync();
        
        return true;
    }
}