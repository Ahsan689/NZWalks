using AutoMapper;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Mapping
{
    public class AutoMapperProfiles: Profile

    {
        public AutoMapperProfiles()
        {
            CreateMap<RegionDto, Region>().ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<AddWalksRequestDto, Walks>().ReverseMap();

            CreateMap<UpdateWalksRequestDto, Walks>().ReverseMap();

            CreateMap<Walks, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
