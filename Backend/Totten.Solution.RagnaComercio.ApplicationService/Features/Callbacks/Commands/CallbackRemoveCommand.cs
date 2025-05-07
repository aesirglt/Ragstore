namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Commands;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class CallbackRemoveCommand : IRequest<Result<Success>>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
