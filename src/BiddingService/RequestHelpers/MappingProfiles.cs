using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts;

namespace BiddingService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bid, BidDto>()
                .ForMember(
                    dest => dest.FreightId,
                    opt => opt.MapFrom(src => src.FreightId.ToString())
                );
            CreateMap<Bid, BidPlaced>()
                .ForMember(
                    dest => dest.FreightId,
                    opt => opt.MapFrom(src => src.FreightId.ToString())
                );
        }
    }
}
