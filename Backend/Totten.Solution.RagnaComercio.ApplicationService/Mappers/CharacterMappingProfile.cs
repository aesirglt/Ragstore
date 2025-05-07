namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers;

using AutoMapper;
using System;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Characters.Commands;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;

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
