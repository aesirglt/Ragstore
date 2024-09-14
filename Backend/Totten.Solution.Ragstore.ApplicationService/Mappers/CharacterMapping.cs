namespace Totten.Solution.Ragstore.ApplicationService.Mappers;

using AutoMapper;
using System;
using Totten.Solution.Ragstore.ApplicationService.Features.Characters.Commands;
using Totten.Solution.Ragstore.Domain.Features.Characters;

/// <summary>
/// 
/// </summary>
public class CharacterMapping : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public CharacterMapping()
    {
        CreateMap<CharacterCreateCommand, Character>()
            .ForMember(ds => ds.IdInServer, m => m.MapFrom(src => src.Id))
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.Now));
    }
}
