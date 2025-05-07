namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

public class SearchedItemMappingProfile : Profile
{
    public SearchedItemMappingProfile()
    {
        CreateMap<SearchedItem, SearchedItemViewModel>()
            .ForMember(ds => ds.ItemName, m => m.MapFrom(src => src.Name));
        CreateMap<SearchedItemViewModel, SearchedItemViewModel>();
    }
}
