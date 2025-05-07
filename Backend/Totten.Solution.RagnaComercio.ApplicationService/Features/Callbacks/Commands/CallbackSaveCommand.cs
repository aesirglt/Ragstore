namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class CallbackSaveCommand : IRequest<Result<Success>>
{
    public Guid ServerId { get; set; }
    public Guid UserId { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
}
