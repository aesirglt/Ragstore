namespace Totten.Solution.Ragstore.ApplicationService.Mappers;

using AutoMapper;
using System;
using Totten.Solution.Ragstore.ApplicationService.Features.Chats.Commands;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

/// <summary>
/// 
/// </summary>
public class ChatMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public ChatMappingProfile()
    {
        CreateMap<ChatCreateCommand, Chat>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow));

        CreateMap<ChatCreateCommand.EquipmentDto, EquipmentItem>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow));

        CreateMap<ChatCreateCommand.EquipmentDto.ItemCardDto, EquipmentItemCardInfo>();
        CreateMap<ChatCreateCommand.EquipmentDto.ItemOptionDto, EquipmentItemOptionInfo>();
    }
}
