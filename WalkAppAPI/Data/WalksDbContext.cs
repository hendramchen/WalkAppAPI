using Microsoft.EntityFrameworkCore;
using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Data;

public class WalksDbContext: DbContext
{
    public WalksDbContext(DbContextOptions<WalksDbContext> options): base(options)
    {
        //
    }
    
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}