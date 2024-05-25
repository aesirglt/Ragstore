namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class ServerDeactiveCommand : IRequest<Result<Unit>>
{
    public int ServerId { get; set; }
}
