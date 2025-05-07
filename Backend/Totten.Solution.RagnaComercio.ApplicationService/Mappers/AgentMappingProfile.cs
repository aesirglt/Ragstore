namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Agents.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Agents;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;

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
            .ForMember(ds => ds.ServerId, m => m.MapFrom(src => src.ServerId))
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.IsActive, m => m.MapFrom(_ => true));

        CreateMap<Agent, AgentResumeViewModel>()
            .ForMember(ds => ds.UpdatedIn, m => m.MapFrom(src => src.UpdatedAt.ToString("o")));
    }
}