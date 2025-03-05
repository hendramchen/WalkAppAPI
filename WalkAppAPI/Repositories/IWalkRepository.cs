using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<Walk?> GetByIdAsync(Guid id);
    Task<List<Walk>> GetAllAsync();
    Task<Walk?> UpdateAsync(Guid id, Walk walk);
    Task<bool> DeleteAsync(Guid id);
}