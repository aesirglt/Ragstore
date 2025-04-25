namespace Totten.Solution.Ragstore.ApplicationService.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public class SearchedItemMappingProfile : Profile
{
    public SearchedItemMappingProfile()
    {
        CreateMap<SearchedItem, SearchedItemViewModel>()
            .ForMember(ds => ds.ItemName, m => m.MapFrom(src => src.Name));
        CreateMap<SearchedItemViewModel, SearchedItemViewModel>();
    }
}
