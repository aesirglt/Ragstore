namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Servers;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

/// <summary>
/// 
/// </summary>
public class ServerMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public ServerMappingProfile()
    {
        CreateMap<ServerCreateCommand, Server>()
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.Name))
            .ForMember(ds => ds.SiteUrl, m => m.MapFrom(src => src.SiteUrl))
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.IsActive, m => m.MapFrom(_ => true));

        CreateMap<Server, ServerResume>()
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.Name));
    }
}