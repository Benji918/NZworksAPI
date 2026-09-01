using AutoMapper;

namespace NZworks.Mappings

{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Domain.Region, Models.DTO.RegionDTO>().ReverseMap();
        }
    }


}
