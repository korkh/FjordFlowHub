using AutoMapper;
using FreightService.DTOs;
using FreightService.Entities;

namespace FreightService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // To Dto from Freight Mappings for Get Operations
            CreateMap<Freight, FreightDto>().IncludeMembers(s => s.Cargo);
            CreateMap<Cargo, FreightDto>();
            //From Dto to Freight Mappings for POST Operations
            CreateMap<CreateFreightDto, Freight>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src));
            CreateMap<CreateFreightDto, Cargo>();
        }
    }
}
