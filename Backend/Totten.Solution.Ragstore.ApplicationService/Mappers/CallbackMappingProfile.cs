namespace Totten.Solution.Ragstore.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

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
        CreateMap<Callback, CallbackResumeViewModel>()
            .ForMember(ds => ds.ServerName, m => m.MapFrom(src => src.Server == null ? string.Empty : src.Server.Name));

        CreateMap<CallbackResumeViewModel, CallbackResumeViewModel>();
        CreateMap<CallbackSaveCommand, Callback>()
            .ForMember(ds => ds.Id, m => m.MapFrom(_ => 0))
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UserId, m => m.MapFrom(src => src.UserId))
            .ForMember(ds => ds.ServerId, m => m.MapFrom(src => src.ServerId))
            .ForMember(ds => ds.ItemId, m => m.MapFrom(src => src.ItemId))
            .ForMember(ds => ds.ItemPrice, m => m.MapFrom(src => src.ItemPrice));
    }
}