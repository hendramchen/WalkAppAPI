using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkAppAPI.Data;
using WalkAppAPI.Models.Domain;

namespace WalkAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController: ControllerBase
{
    private readonly WalksDbContext _context;
    
    public RegionsController(WalksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    {
        return await _context.Regions.ToListAsync();
    }
}