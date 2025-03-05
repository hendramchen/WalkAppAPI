using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkAppAPI.Data;
using WalkAppAPI.Models.Domain;
using WalkAppAPI.Models.DTO;
using WalkAppAPI.Repositories;

namespace WalkAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController: ControllerBase
{
    private readonly WalksDbContext _context;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    
    public RegionsController(WalksDbContext context, IRegionRepository regionRepository, IMapper mapper)
    {
        _context = context;
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegionDto>>> GetAll()
    {
        // var regions = await _context.Regions.ToListAsync();
        var regions = await _regionRepository.GetAllRegions();
        // return DTO
        return Ok(_mapper.Map<IEnumerable<RegionDto>>(regions));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RegionDto>> GetById(Guid id)
    {
        var region = await _regionRepository.GetRegionById(id);

        if (region == null)
        {
            return NotFound();
        }

        return _mapper.Map<RegionDto>(region);
    }

    [HttpPost]
    public async Task<ActionResult<RegionDto>> PostRegion(AddRegionRequestDto addRegionRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        // map or convert DTO to domain model
        var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);
        
        // use domain model to create region
        regionDomainModel = await _regionRepository.CreateRegion(regionDomainModel);
        
        // map domain model back to DTO
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
        
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RegionDto>> PutRegion(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        // map DTO to domain model
        var region = _mapper.Map<Region>(updateRegionRequestDto);
        region = await _regionRepository.UpdateRegion(id, region);
        if (region == null)
        {
            return NotFound();
        }
        // convert domain model to DTO
        var regionDto = _mapper.Map<RegionDto>(region);

        return regionDto;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<RegionDto>> DeleteRegion(Guid id)
    {
        var region = await _regionRepository.DeleteRegion(id);
        if (!region)
        {
            return NotFound();
        }

        return NoContent();
    }
}