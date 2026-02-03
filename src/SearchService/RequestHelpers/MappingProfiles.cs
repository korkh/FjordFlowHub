using AutoMapper;
using Contracts;
using SearchService.Models;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Simple mapping because property names in FreightCreated
        // match property names in Item
        CreateMap<FreightCreated, Item>();

        // Similarly for updates
        CreateMap<FreightUpdated, Item>();
    }
}
