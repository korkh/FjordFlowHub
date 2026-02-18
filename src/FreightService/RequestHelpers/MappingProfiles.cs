using AutoMapper;
using Contracts;
using FreightService.DTOs;
using FreightService.Entities;

namespace FreightService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // 1. From Entity into FreightDto (for GET-requests)
            // Flattening works automatically because we include Cargo members
            CreateMap<Freight, FreightDto>().IncludeMembers(s => s.Cargo);
            CreateMap<Cargo, FreightDto>();

            // 2. From DTO into Entity (for POST)
            // English comment: Ensure all dimension fields (Length, Width, Height)
            // from CreateFreightDto are mapped to the Cargo entity.
            CreateMap<CreateFreightDto, Freight>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src));
            CreateMap<CreateFreightDto, Cargo>();

            // 3. From DTO into Contracts (publishing FreightCreated to the bus)
            CreateMap<FreightDto, FreightCreated>();

            // 4. From Entity into Contracts (publishing FreightUpdated to the bus)
            // This is crucial for SearchService to have updated dimensions
            CreateMap<Freight, FreightUpdated>().IncludeMembers(a => a.Cargo);
            CreateMap<Cargo, FreightUpdated>();

            // 5. Mapping for FreightFinished
            CreateMap<Freight, FreightFinished>()
                .ForMember(dest => dest.FreightId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.SoldAmount))
                .ForMember(
                    dest => dest.FreightSold,
                    opt => opt.MapFrom(src => src.Status == Status.Finished)
                );
        }
    }
}
