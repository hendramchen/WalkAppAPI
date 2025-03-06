using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkAppAPI.CustomActionFilters;
using WalkAppAPI.Models.Domain;
using WalkAppAPI.Models.DTO;
using WalkAppAPI.Repositories;

namespace WalkAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _repository;

    public WalksController(IMapper mapper, IWalkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<WalkDto>>> GetAll(
        [FromQuery] string? filterOn, 
        [FromQuery] string? filterQuery, 
        [FromQuery] string? sortBy, 
        [FromQuery] bool? isAsc,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 1000
        )
    {
        var wakls = await _repository.GetAllAsync(filterOn, filterQuery, sortBy, isAsc ?? true, pageNumber, pageSize);
        return Ok(_mapper.Map<IEnumerable<WalkDto>>(wakls));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var walk = await _repository.GetByIdAsync(id);
        if (walk == null) return NotFound();
        return Ok(_mapper.Map<WalkDto>(walk));
    }

    [HttpPost]
    [ValidateModel] // another way to validate request with custom validation
    public async Task<ActionResult<WalkDto>> CreateWalk(AddWalkRequestDto addWalkRequestDto)
    {
        var walk = _mapper.Map<Walk>(addWalkRequestDto);
        await _repository.CreateAsync(walk);
        return _mapper.Map<WalkDto>(walk);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WalkDto>> UpdateWalk(Guid id, UpdateWalkRequestDto updateWalkRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var walk = _mapper.Map<Walk>(updateWalkRequestDto);
        walk = await _repository.UpdateAsync(id, walk);
        if (walk == null) return NotFound();
        return _mapper.Map<WalkDto>(walk);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isWalkRemoved = await _repository.DeleteAsync(id);
        if (!isWalkRemoved) NotFound();
        return NoContent();
    }
}