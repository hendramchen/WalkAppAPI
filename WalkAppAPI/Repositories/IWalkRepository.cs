using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<Walk?> GetByIdAsync(Guid id);
    Task<List<Walk>> GetAllAsync(
        string? filterOn = null, 
        string? filterQuery = null, 
        string? sortBy = null, 
        bool isAsc = true,
        int pageNumber = 1,
        int pageSize = 1000
        );
    Task<Walk?> UpdateAsync(Guid id, Walk walk);
    Task<bool> DeleteAsync(Guid id);
}