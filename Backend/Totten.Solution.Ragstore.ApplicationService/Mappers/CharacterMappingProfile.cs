namespace Totten.Solution.Ragstore.ApplicationService.Mappers;

using AutoMapper;
using System;
using Totten.Solution.Ragstore.ApplicationService.Features.Characters.Commands;
using Totten.Solution.Ragstore.Domain.Features.Characters;

/// <summary>
/// 
/// </summary>
public class CharacterMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public CharacterMappingProfile()
    {
        CreateMap<CharacterCreateCommand, Character>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow));
    }
}
