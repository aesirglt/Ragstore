namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.CrossDTOs;
using Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Callbacks;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Items;

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
        CreateMap<Callback, ItemResumeViewModel>();

        CreateMap<(CallbackCreateDto dto, UserData data), CallbackSaveCommand>()
            .ForMember(ds => ds.Server, m => m.MapFrom(src => src.dto.Server))
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.dto.Name))
            .ForMember(ds => ds.UserId, m => m.MapFrom(src => src.data.Id))
            .ForMember(ds => ds.UserCellphone, m => m.MapFrom(src => src.data.Cellphone))
            .ForMember(ds => ds.Level, m => m.MapFrom(src => $"{src.data.Level}"))
            .ForMember(ds => ds.Items, m => m.MapFrom(src => src.dto.ItemByValue));

        CreateMap<CallbackSaveCommand, Callback>()
            .ForMember(ds => ds.Id, m => m.MapFrom(src => $"{Guid.NewGuid()}"))
            .ForMember(ds => ds.CallbackOwnerId, m => m.MapFrom(src => src.UserId))
            .ForMember(ds => ds.UserCellphone, m => m.MapFrom(src => src.UserCellphone))
            .ForMember(ds => ds.Server, m => m.MapFrom(src => src.Server))
            .ForMember(ds => ds.Items, m => m.MapFrom(src => src.Items))
            .ForMember(ds => ds.Level, m => m.MapFrom(src => src.Level));
    }
}