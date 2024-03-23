namespace Totten.Solution.Ragstore.WebApi.Mappers.StoreAggregation;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
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
        CreateMap<VendingStore, StoreResumeViewModel>()
            .ForMember(ds => ds.Guid, m => m.MapFrom(src => src.Id));
        CreateMap<VendingStore, StoreDetailViewModel>()
            .ForMember(ds => ds.Guid, m => m.MapFrom(src => src.Id));

        CreateMap<VendingStoreSaveCommand, VendingStore>()
            .ForMember(ds => ds.VendingStoreItems, m => m.MapFrom(src => src.VendingStoreItems));
    }
}