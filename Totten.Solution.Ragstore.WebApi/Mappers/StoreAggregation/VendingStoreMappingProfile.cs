namespace Totten.Solution.Ragstore.WebApi.Mappers.StoreAggregation;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public class VendingStoreMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public VendingStoreMappingProfile()
    {
        CreateMap<VendingStore, StoreResumeViewModel>();

        CreateMap<VendingStore, StoreDetailViewModel>()
            .ForMember(ds => ds.Items, m => m.MapFrom(src => src.VendingStoreItems.ToDictionary(l => l.Id, l => l.Name ?? string.Empty)));

        CreateMap<VendingStoreSaveCommand, VendingStore>()
            .ForMember(ds => ds.VendingStoreItems, m => m.MapFrom(src => src.VendingStoreItems))
            .ForMember(ds => ds.VendingStoreItems, m => m.MapFrom(src => src.VendingStoreItems));
        CreateMap<VendingStore, VendingStore>();
    }
}