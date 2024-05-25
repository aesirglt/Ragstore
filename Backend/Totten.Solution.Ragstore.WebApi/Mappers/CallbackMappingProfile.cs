namespace Totten.Solution.Ragstore.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.Infra.Cross.CrossDTOs;
using Totten.Solution.Ragstore.WebApi.Dtos.Callbacks;

/// <summary>
/// 
/// </summary>
public class CallbackMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public CallbackMappingProfile()
    {
        CreateMap<(CallbackCreateDto dto, UserData data), CallbackSaveCommand>()
            .ForMember(ds => ds.Server, m => m.MapFrom(src => src.dto.Server))
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.dto.Name))
            .ForMember(ds => ds.UserId, m => m.MapFrom(src => src.data.Id))
            .ForMember(ds => ds.UserCellphone, m => m.MapFrom(src => src.data.Cellphone))
            .ForMember(ds => ds.Level, m => m.MapFrom(src => $"{src.data.Level}"))
            .ForMember(ds => ds.ItemId, m => m.MapFrom(src => src.dto.ItemId))
            .ForMember(ds => ds.ItemPrice, m => m.MapFrom(src => src.dto.ItemPrice));
    }
}