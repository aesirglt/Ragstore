namespace Totten.Solution.Ragstore.ApplicationService.Mappers.StoreAggregation;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;

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
            .ForMember(ds => ds.Items,
                       m => m.MapFrom(src => src.VendingStoreItems
                                                .ToDictionary(l => l.ItemId,
                                                              l => new StoreDetailViewModel.ItemDetail
                                                              {
                                                                  Name = l.Name ?? "not null guard",
                                                                  Price = l.Price
                                                              })))
            .ForMember(ds => ds.Character, m => m.MapFrom(src => src.Character == null ? "" : src.Character.Name));

        CreateMap<VendingStoreSaveCommand, VendingStore>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.VendingStoreItems, m => m.MapFrom(src => src.StoreItems))
            .ForMember(ds => ds.VendingStoreItems, m => m.MapFrom(src => src.StoreItems));

        CreateMap<VendingStore, VendingStore>();
    }
}