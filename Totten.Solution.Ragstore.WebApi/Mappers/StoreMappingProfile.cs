namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using System.Linq;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
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
        CreateMap<VendingStore, StoreResumeViewModel>()
            .ForMember(ds => ds.Guid, m => m.MapFrom(src => src.Id))
            .ForMember(ds => ds.Title, m => m.MapFrom(src => src.Name));
        CreateMap<VendingStore, StoreDetailViewModel>()
            .ForMember(ds => ds.Guid, m => m.MapFrom(src => src.Id))
            .ForMember(ds => ds.Title, m => m.MapFrom(src => src.Name));
        CreateMap<StoreCreateDto, StoreSaveCommand>()
            .ForMember(ds => ds.CreationDate, m => m.MapFrom(src => DateTime.Now));
        CreateMap<StoreSaveCommand, VendingStore>()
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.Title))
            .ForMember(ds => ds.Id, m => m.MapFrom(src => Guid.NewGuid()));
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