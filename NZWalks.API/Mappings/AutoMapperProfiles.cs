using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Region,RegionDetailsDTO>().ReverseMap();
            CreateMap<CreateRegionRequestDTO, Region>().ReverseMap();
        }
    }
}
