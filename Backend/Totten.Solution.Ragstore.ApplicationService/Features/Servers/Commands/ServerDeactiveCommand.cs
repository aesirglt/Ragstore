namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;


public class ServerDeactiveCommand : IRequest<Result<Success>>
{
    public int ServerId { get; set; }
}
