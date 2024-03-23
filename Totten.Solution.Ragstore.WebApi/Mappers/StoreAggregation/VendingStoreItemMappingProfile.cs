namespace Totten.Solution.Ragstore.WebApi.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreItemMappingProfile : Profile
{
    public VendingStoreItemMappingProfile()
    {
        CreateMap<VendingStoreItemCommand, VendingStoreItem>()
            .ForMember(ds => ds.VendingStore, m => m.Ignore())
            .ForMember(ds => ds.InfoCards, m => m.MapFrom(src => src.InfoCards))
            .ForMember(ds => ds.InfoOptions, m => m.MapFrom(src => src.InfoOptions));
    }
}
