namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

public class SearchedItemMappingProfile : Profile
{
    public SearchedItemMappingProfile()
    {
        CreateMap<SearchedItem, SearchedItemViewModel>()
            .ForMember(ds => ds.ItemName, m => m.MapFrom(src => src.Name))
            .ForMember(ds => ds.ItemId, m => m.MapFrom(src => src.ItemId))
            .ForMember(ds => ds.Average, m => m.MapFrom(src => src.Average))
            .ForMember(ds => ds.Quantity, m => m.MapFrom(src => src.Quantity))
            .ForMember(ds => ds.Image, m => m.MapFrom(src => "https://static.divine-pride.net/images/items/item/" + src.ItemId + ".png"));

        CreateMap<SearchedItemViewModel, SearchedItemViewModel>();
    }
}
