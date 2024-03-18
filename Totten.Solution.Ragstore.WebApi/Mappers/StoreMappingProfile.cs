namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using System.Linq;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Stores;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public class StoreMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public StoreMappingProfile()
    {
        CreateMap<Store, StoreResumeViewModel>()
            .ForMember(ds => ds.Guid, m => m.MapFrom(src => src.Id))
            .ForMember(ds => ds.Title, m => m.MapFrom(src => src.Name));
        CreateMap<Store, StoreDetailViewModel>()
            .ForMember(ds => ds.Guid, m => m.MapFrom(src => src.Id))
            .ForMember(ds => ds.Title, m => m.MapFrom(src => src.Name))
            .ForMember(ds => ds.Items, m => m.MapFrom(src => ToDictionary(src.Items)));
        CreateMap<StoreCreateDto, StoreSaveCommand>()
            .ForMember(ds => ds.CreationDate, m => m.MapFrom(src => DateTime.Now));
        CreateMap<StoreSaveCommand, Store>()
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.Title))
            .ForMember(ds => ds.Id, m => m.MapFrom(src => Guid.NewGuid()))
            .ForMember(ds => ds.Merchant, m => m.MapFrom(src => src.SellerName))
            .ForMember(ds => ds.Items, m => m.MapFrom(src => string.Join('#', src.Items.Select(x => $"{x.Key}:{x.Value}"))));
    }

    private Dictionary<string, double> ToDictionary(string items)
    {
        var splited = items.Split('#', StringSplitOptions.RemoveEmptyEntries);

        var listKey = splited.Select(itemPrice =>
        {
            var splited = itemPrice.Split(':', StringSplitOptions.RemoveEmptyEntries);
            return new KeyValuePair<string, double>(splited[0], double.Parse(splited[1]));
        });

        return listKey.ToDictionary();
    }
}