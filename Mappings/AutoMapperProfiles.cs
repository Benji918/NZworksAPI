using AutoMapper;

namespace NZworks.Mappings

{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Domain.Region, Models.DTO.RegionDTO>().ReverseMap();
            CreateMap<Models.Domain.Region, Models.DTO.AddRegionRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Walk, Models.DTO.AddWalkRequestDTO>().ReverseMap();
        }
    }


}
