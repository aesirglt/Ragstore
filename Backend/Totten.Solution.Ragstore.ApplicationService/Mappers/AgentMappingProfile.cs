namespace Totten.Solution.Ragstore.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Items;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;

/// <summary>
/// 
/// </summary>
public class AgentMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public AgentMappingProfile()
    {
        CreateMap<AgentCreateCommand, Agent>()
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.Name))
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.IsActive, m => m.MapFrom(_ => true));

        CreateMap<Agent, ItemResumeViewModel>();
    }
}