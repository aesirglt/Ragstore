namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;


public class ServerCreateCommand : IRequest<Result<Success>>
{
    public string Name { get; set; } = string.Empty;
    public string SiteUrl { get; set; } = string.Empty;
}
