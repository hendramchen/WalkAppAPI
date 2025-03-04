using AutoMapper;
using WalkAppAPI.Models.Domain;
using WalkAppAPI.Models.DTO;

namespace WalkAppAPI.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
    }
}