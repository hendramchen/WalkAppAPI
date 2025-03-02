using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkAppAPI.Data;
using WalkAppAPI.Models.Domain;
using WalkAppAPI.Models.DTO;

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
    public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegions()
    {
        var regions = await _context.Regions.ToListAsync();
        // map domain models to DTOs
        var regionDTOs = new List<RegionDto>();
        foreach (var region in regions)
        {
            regionDTOs.Add(new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            });
        }
        return Ok(regionDTOs);
    }
    //public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    //{
        //return await _context.Regions.ToListAsync();
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<RegionDto>> GetRegion(Guid id)
    {
        Region region = await _context.Regions.FindAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        RegionDto regionDto = new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl
        };
        return regionDto;
    }

    [HttpPost]
    public async Task<ActionResult<RegionDto>> PostRegion(AddRegionRequestDto addRegionRequestDto)
    {
        // map or convert DTO to domain model
        var regionDomainModel = new Region()
        {
            Name = addRegionRequestDto.Name,
            Code = addRegionRequestDto.Code,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl,
        };
        
        // use domain model to create region
        _context.Regions.Add(regionDomainModel);
        await _context.SaveChangesAsync();
        
        // map domain model back to DTO
        RegionDto regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl,
        };
        
        return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RegionDto>> PutRegion(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
    {
        Region region = await _context.Regions.FindAsync(id);
        if (region == null)
        {
            return NotFound();
        }
        // map DTO to domain model
        region.Name = updateRegionRequestDto.Name;
        region.Code = updateRegionRequestDto.Code;
        region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
        await _context.SaveChangesAsync();
        // convert domain model to DTO
        RegionDto regionDto = new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl,
        };
        
        return regionDto;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<RegionDto>> DeleteRegion(Guid id)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region == null)
        {
            return NotFound();
        }
        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}