namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class ServerCreateCommand : IRequest<Result<Unit>>
{
    public string Name { get; set; } = string.Empty;
    public string SiteUrl { get; set; } = string.Empty;
}
