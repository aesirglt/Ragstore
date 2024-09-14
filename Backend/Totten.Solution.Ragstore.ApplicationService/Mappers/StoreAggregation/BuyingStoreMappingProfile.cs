namespace Totten.Solution.Ragstore.ApplicationService.Mappers.StoreAggregation;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;

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
            .ForMember(ds => ds.Character, m => m.MapFrom(src => src.Character == null ? "" : src.Character.Name))
            .ForMember(
                ds => ds.Items,
                m => m.MapFrom(src =>
                src.BuyingStoreItem == null
                ? new()
                : new Dictionary<int, StoreDetailViewModel.ItemDetail>
                {
                    {
                        src.BuyingStoreItem.ItemId,
                        new()
                        {
                            Name = src.BuyingStoreItem.Name,
                            Price = src.BuyingStoreItem.Price
                        }
                    }
                }));

        CreateMap<BuyingStoreSaveCommand, BuyingStore>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.Now));
        CreateMap<BuyingStore, BuyingStore>();
    }
}