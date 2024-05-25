namespace Totten.Solution.Ragstore.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Servers;

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
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.Now))
            .ForMember(ds => ds.IsActive, m => m.MapFrom(_ => true));

        CreateMap<Server, ServerResume>()
            .ForMember(ds => ds.Name, m => m.MapFrom(src => src.Name));
        CreateMap<Server, ServerVerifyDTO>()
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => src.UpdatedAt));
    }
}