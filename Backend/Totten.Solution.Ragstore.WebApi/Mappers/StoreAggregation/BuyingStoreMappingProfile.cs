namespace Totten.Solution.Ragstore.WebApi.Mappers.StoreAggregation;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.WebApi.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public class BuyingStoreMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public BuyingStoreMappingProfile()
    {
        CreateMap<BuyingStore, StoreResumeViewModel>();

        CreateMap<BuyingStore, StoreDetailViewModel>()
            .ForMember(ds => ds.Character, m => m.MapFrom(src => src.Character == null ? "" : src.Character.Name));

        CreateMap<BuyingStoreSaveCommand, BuyingStore>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.Now));
        CreateMap<BuyingStore, BuyingStore>();
    }
}