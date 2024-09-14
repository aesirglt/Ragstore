namespace Totten.Solution.Ragstore.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Items;
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
        CreateMap<Callback, ItemResumeViewModel>();
        CreateMap<CallbackSaveCommand, Callback>()
            .ForMember(ds => ds.Id, m => m.MapFrom(_ => 0))
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.CallbackOwnerId, m => m.MapFrom(src => src.UserId))
            .ForMember(ds => ds.UserCellphone, m => m.MapFrom(src => src.UserCellphone))
            .ForMember(ds => ds.Server, m => m.MapFrom(src => src.Server))
            .ForMember(ds => ds.ItemId, m => m.MapFrom(src => src.ItemId))
            .ForMember(ds => ds.ItemPrice, m => m.MapFrom(src => src.ItemPrice))
            .ForMember(ds => ds.Level, m => m.MapFrom(src => src.Level));
    }
}