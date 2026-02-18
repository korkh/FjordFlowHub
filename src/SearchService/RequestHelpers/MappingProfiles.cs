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

        // Updating dimensions is crucial for SearchService to have accurate data
        CreateMap<FreightUpdated, Item>();
    }
}
